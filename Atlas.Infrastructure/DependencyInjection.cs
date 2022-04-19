using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Interfaces;

namespace Atlas.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<AtlasDbContext>(options =>
            {
                options.UseNpgsql(connectionString, options =>
                {
                    options.CommandTimeout(100);
                    options.EnableRetryOnFailure();
                    options.SetPostgresVersion(new Version(9, 6));
                });
            });

            services.AddScoped<IAtlasDbContext>(provider =>
                provider.GetService<AtlasDbContext>());

            return services;
        }
    }
}
