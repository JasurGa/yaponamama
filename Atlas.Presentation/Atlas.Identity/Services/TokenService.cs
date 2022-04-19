using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Atlas.Identity.Constants;
using Atlas.Identity.Models;
using Atlas.Identity.Settings;
using Microsoft.Extensions.Options;

namespace Atlas.Identity.Services
{
    public class TokenService
    {
        private readonly TokenGenerationSettings _tokenGenerationSettings;

        public TokenService(IOptions<TokenGenerationSettings> tokenGenerationSettings) =>
            _tokenGenerationSettings = tokenGenerationSettings.Value;

        public AuthToken GenerateToken(Claim[] claims)
        {
            var expiresAt = DateTime.UtcNow.AddDays(1);

            var jwt = new JwtSecurityToken
            (
                issuer             : _tokenGenerationSettings.Issuer,
                audience           : _tokenGenerationSettings.Audience,
                signingCredentials : _tokenGenerationSettings.SigningCredentials,
                claims             : claims,
                expires            : expiresAt
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AuthToken
            {
                TokenType = TokenInfo.TokenType,
                Token     = token,
                ExpiresAt = expiresAt,
            };
        }
    }
}
