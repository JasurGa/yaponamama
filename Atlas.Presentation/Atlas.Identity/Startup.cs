using System;
using System.Diagnostics;
using System.Text;
using Atlas.Application;
using Atlas.Eskiz;
using Atlas.Identity.Extensions;
using Atlas.Identity.Middlewares;
using Atlas.Identity.Services;
using Atlas.Identity.Settings;
using Atlas.Persistence;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Atlas.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEskiz(Configuration);

            var smsSettings = Configuration.GetSection("SmsSettings");

            var accountSid          = smsSettings.GetValue<string>("AccountSid");
            var authToken           = smsSettings.GetValue<string>("AuthToken");
            var fromNumber          = smsSettings.GetValue<string>("FromPhoneNumber");
            var messagingServiceSid = smsSettings.GetValue<string>("MessagingServiceSid");

            services.Configure<SmsSettings>(op =>
            {
                op.AccountSid          = accountSid;
                op.AuthToken           = authToken;
                op.FromPhoneNumber     = fromNumber;
                op.MessagingServiceSid = messagingServiceSid;
            });

            services.AddScoped<SmsService>();

            var tokenGenerationSettings = Configuration.GetSection("TokenGenerationSettings");

            var secret        = tokenGenerationSettings.GetValue<string>("Secret");
            var issuer        = tokenGenerationSettings.GetValue<string>("Issuer");
            var audience      = tokenGenerationSettings.GetValue<string>("Audience");
            var tokenType     = tokenGenerationSettings.GetValue<string>("TokenType");
            var tokenHeader   = tokenGenerationSettings.GetValue<string>("TokenHeader");
            var encryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            services.Configure<TokenGenerationSettings>(op =>
            {
                op.Secret             = secret;
                op.Issuer             = issuer;
                op.Audience           = audience;
                op.SigningCredentials = new SigningCredentials(encryptionKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddScoped<TokenService>();
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
                });

            services.AddControllers();
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddSwagger(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
            });

            services.AddScheduler();

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "AtlasWebApi");
                op.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });

            app.UseOptionsMiddleware();
            app.UseCustomExceptionHandler();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health");
        }
    }
}