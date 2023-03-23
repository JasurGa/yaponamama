using System;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.SubscribeApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSubscribeApi(this IServiceCollection services,
            IConfiguration Configuration)
        {
            var section = Configuration.GetSection(SubscribeSettings.SubscribeSection);

            var url       = section.GetValue<string>("Url");
            var authToken = section.GetValue<string>("AuthToken");

            services.Configure<SubscribeSettings>(o =>
            {
                o.Url       = url;
                o.AuthToken = authToken;
            });

            services.AddScoped<ISubscribeClient, SubscribeClient>();
            return services;
        }
    }
}

