using System;
using Atlas.Eskiz.Abstractions;
using Atlas.Eskiz.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.Eskiz
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEskiz(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddScoped<IEskizClient, EskizClient>();

            var section = Configuration.GetSection(EskizSettings.EskizSection);

            var email      = section.GetValue<string>("Email");
            var password   = section.GetValue<string>("Password");
            var fromNumber = section.GetValue<string>("FromNumber");

            services.Configure<EskizSettings>(o =>
            {
                o.Email      = email;
                o.Password   = password;
                o.FromNumber = fromNumber;
            });

            return services;
        }
    }
}

