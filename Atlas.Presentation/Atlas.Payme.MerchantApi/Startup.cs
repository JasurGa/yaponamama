using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Atlas.Application;
using Atlas.Payme.MerchantApi.Controllers;
using Atlas.Payme.MerchantApi.Helpers.Policies;
using Atlas.Payme.MerchantApi.Middlewares;
using Atlas.Payme.MerchantApi.Models;
using Atlas.Payme.MerchantApi.Services;
using Atlas.Payme.MerchantApi.Settings;
using Atlas.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Atlas.Payme.MerchantApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddJsonRpc(config =>
            {
                config.ShowServerExceptions = true;
                config.JsonSerializerSettings = new System.Text.Json.JsonSerializerOptions
                {
                    IgnoreNullValues     = false,
                    WriteIndented        = true,
                    PropertyNamingPolicy = new SnakeCaseNamingPolicy()
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Atlas.Payme.MerchantApi", Version = "v1" });
            });
            services.AddPersistence(Configuration);

            services.Configure<AuthSettings>(
                Configuration.GetSection(AuthSettings.Auth));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Atlas.Payme.MerchantApi v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<BasicAuthMiddleware>();

            app.UseJsonRpc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

