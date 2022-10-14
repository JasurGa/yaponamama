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
            var section = Configuration.GetSection(EskizSettings.EskizSection);

            var email      = section.GetValue<string>("Email");
            var password   = section.GetValue<string>("Password");
            var fromNumber = section.GetValue<string>("FromNumber");

            Console.WriteLine("Eskiz.AddSettings Email: " + email);
            Console.WriteLine("Eskiz.AddSettings Password: " + password);
            Console.WriteLine("Eskiz.AddSettings FromNumber: " + fromNumber);

            services.Configure<EskizSettings>(o =>
            {
                o.Email      = email;
                o.Password   = password;
                o.FromNumber = fromNumber;
            });

            services.AddScoped<IEskizClient, EskizClient>();
            return services;
        }
    }
}

