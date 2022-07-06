using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Atlas.WebApi.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Atlas.WebApi.Extensions
{
    public static class SwaggerDepedencyInjection
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            var settings = Configuration.GetSection("TokenGenerationSettings");

            var tokenType   = settings.GetValue<string>("TokenType");
            var tokenHeader = settings.GetValue<string>("TokenHeader");

            services.AddRouting(config =>
            {
                config.LowercaseUrls         = true;
                config.LowercaseQueryStrings = true;
            });

            services.AddSwaggerGen(op =>
            {
                op.DocumentFilter<HideInDocsFilter>();
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version     = "v1",
                    Title       = "AtlasWebApi",
                    Description = "AtlasWebApi",

                    Contact = new OpenApiContact()
                    {
                        Name  = "",
                        Email = "",
                    }
                });

                op.IgnoreObsoleteActions();
                op.IgnoreObsoleteProperties();

                op.AddSecurityDefinition(tokenType, new OpenApiSecurityScheme()
                {
                    Description = "<type> <token>",
                    Name        = tokenHeader,
                    In          = ParameterLocation.Header,
                    Type        = SecuritySchemeType.ApiKey,
                    Scheme      = tokenType,
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id   = tokenType,
                                Type = ReferenceType.SecurityScheme
                            },
                            Name   = tokenType,
                            In     = ParameterLocation.Header,
                            Scheme = "oauth2"
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                op.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
