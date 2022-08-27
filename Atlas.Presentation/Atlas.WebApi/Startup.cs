using System;
using System.Reflection;
using System.Text;
using Atlas.Application;
using Atlas.Application.Common.Mappings;
using Atlas.Application.Interfaces;
using Atlas.Persistence;
using Atlas.WebApi.Extensions;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Atlas.WebApi.Observers;
using Coravel;
using Atlas.WebApi.Services;
using Atlas.WebApi.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Atlas.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(
                    Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(
                    typeof(IAtlasDbContext).Assembly));
            });

            services.AddControllers();
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddSwagger(Configuration);

            services.AddApiVersioning();

            var tokenGenerationSettings = Configuration.GetSection("TokenGenerationSettings");

            var secret        = tokenGenerationSettings.GetValue<string>("Secret");
            var issuer        = tokenGenerationSettings.GetValue<string>("Issuer");
            var audience      = tokenGenerationSettings.GetValue<string>("Audience");
            var tokenType     = tokenGenerationSettings.GetValue<string>("TokenType");
            var tokenHeader   = tokenGenerationSettings.GetValue<string>("TokenHeader");
            var encryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            services.AddAuthentication(op =>
                {
                    op.DefaultAuthenticateScheme = tokenType;
                    op.DefaultChallengeScheme    = tokenType;
                })
                .AddJwtBearer(op =>
                {
                    op.SaveToken            = true;
                    op.RequireHttpsMetadata = false;

                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey         = encryptionKey,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer    = issuer,
                        ValidateIssuer = true,

                        ValidAudience    = audience,
                        ValidateAudience = true,

                        ClockSkew        = TimeSpan.Zero,
                        ValidateLifetime = false
                    };

                    op.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var path = context.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/api/chuthub"))
                            {
                                var accessToken = context.Request.Query["access_token"];
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    context.Token = accessToken;
                                }

                                accessToken = context.Request.Headers["Authorization"];
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    context.Token = accessToken;
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
            });

            services.AddScoped<DiagnosticObserver>();
            services.AddTransient<UptimeService>();
            services.AddScheduler();

            services.AddHealthChecks();

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            DiagnosticListener diagnosticListenerSource, DiagnosticObserver diagnosticObserver)
        {
            diagnosticListenerSource.Subscribe(diagnosticObserver);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "AtlasWebApi");
            });

            app.UseOptionsMiddleware();
            app.UseCustomExceptionHandler();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseApiVersioning();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/api/chathub");
            });

            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler
                    .Schedule<UptimeService>()
                    .EveryFiveSeconds();
            });

            app.UseHealthChecks("/health");
        }
    }
}
