using System;
using System.Text;
using Atlas.Identity.Constants;
using Atlas.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Atlas.Identity.Services
{
    public static class JwtTokenExtensions
    {
        public static IServiceCollection AddToken(this IServiceCollection services,
            IConfiguration Configuration)
        {
            var tokenGenerationSettings = Configuration.GetSection("TokenGenerationSettings");

            var secret        = tokenGenerationSettings.GetValue<string>("Secret");
            var issuer        = tokenGenerationSettings.GetValue<string>("Issuer");
            var audience      = tokenGenerationSettings.GetValue<string>("Audience");
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
                    op.DefaultAuthenticateScheme = TokenInfo.TokenType;
                    op.DefaultChallengeScheme    = TokenInfo.TokenType;
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

            return services;
        }
    }
}
