using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Interfaces;
using Neo4j.Driver;
using Microsoft.Extensions.Configuration;
using Atlas.Application.Services;

namespace Atlas.Persistence
{
    public static class DependencyInjection
    {
        public class Neo4jSection
        {
            public string ConnectionString { get; set; }

            public string Login { get; set; }

            public string Password { get; set; }

            public Neo4jSection(IConfiguration configuration)
            {
                ConnectionString = configuration["Neo4j:ConnectionString"];
                Login            = configuration["Neo4j:Login"];
                Password         = configuration["Neo4j:Password"];
            }
        }

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

            services.AddScoped<IAtlasInfluxDbService>(provider =>
                provider.GetService<AtlasInfluxDbService>());

            services.AddScoped<IAtlasDbContext>(provider =>
                provider.GetService<AtlasDbContext>());

            var neo4jConnection = new Neo4jSection(configuration);
            services.AddSingleton(GraphDatabase.Driver(neo4jConnection.ConnectionString,
                AuthTokens.Basic(neo4jConnection.Login, neo4jConnection.Password)));

            return services;
        }
    }
}
