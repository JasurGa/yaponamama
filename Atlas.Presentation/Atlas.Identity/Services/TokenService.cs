using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Atlas.Identity.Constants;
using Atlas.Identity.Models;
using Atlas.Identity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Atlas.Identity.Services
{
    public class TokenService
    {
        private readonly IAtlasDbContext         _dbContext;
        private readonly TokenGenerationSettings _tokenGenerationSettings;

        public TokenService(IAtlasDbContext dbContext, IOptions<TokenGenerationSettings> tokenGenerationSettings) =>
            (_dbContext, _tokenGenerationSettings) = (dbContext, tokenGenerationSettings.Value);

        private static string GenerateRandStr()
        {
            Random random = new();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<AuthToken> GetTokenByUserIdAsync(Guid userId)
        {
            var claims = new List<Claim>();

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            var recruiter = await _dbContext.HeadRecruiters.FirstOrDefaultAsync(x =>
                x.UserId == userId);

            if (client != null)
                claims.Add(new Claim(TokenClaims.ClientId, client.Id.ToString()));

            if (courier != null)
                claims.Add(new Claim(TokenClaims.CourierId, courier.Id.ToString()));

            if (support != null)
                claims.Add(new Claim(TokenClaims.SupportId, support.Id.ToString()));

            if (supplyManager != null)
                claims.Add(new Claim(TokenClaims.SupplyManagerId, support.Id.ToString()));

            if (admin != null)
                claims.Add(new Claim(TokenClaims.AdminId, admin.Id.ToString()));

            if (recruiter != null)
                claims.Add(new Claim(TokenClaims.HeadRecruiterId, recruiter.Id.ToString()));

            claims.Add(new Claim(TokenClaims.UserId, userId.ToString()));

            var refreshToken = GenerateRefreshToken(userId);
            var token = GenerateToken(refreshToken, claims.ToArray());

            return token;
        }

        public AuthToken GenerateToken(RefreshToken refreshToken, Claim[] claims)
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
                TokenType    = TokenInfo.TokenType,
                Token        = token,
                ExpiresAt    = expiresAt,
                RefreshToken = refreshToken.Refresh
            };
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            string refreshStr;
            do
            {
                refreshStr = GenerateRandStr();
            }
            while (_dbContext.RefreshTokens.FirstOrDefault(x => x.Refresh == refreshStr) != null);

            var refreshToken = new RefreshToken
            {
                Id        = Guid.NewGuid(),
                UserId    = userId,
                Refresh   = refreshStr,
                ExpiresAt = DateTime.UtcNow.AddYears(10),
            };

            _dbContext.RefreshTokens.Add(refreshToken);
            _dbContext.SaveChanges();

            return refreshToken;
        }
    }
}
