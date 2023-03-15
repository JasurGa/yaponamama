using System;
using Atlas.SubscribeApi.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.SubscribeApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEskiz(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddScoped<ISubscribeClient, SubscribeClient>();
            return services;
        }
    }
}

