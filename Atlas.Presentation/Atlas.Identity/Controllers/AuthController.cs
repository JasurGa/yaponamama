using Atlas.Domain;
using Atlas.Identity.Models;
using Atlas.Identity.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using Atlas.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Atlas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Constants;
using Atlas.Application.Common.Exceptions;
using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace Atlas.Identity.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAtlasDbContext _dbContext;

        private readonly TokenService _tokenService;

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(TokenClaims.UserId).Value);

        public AuthController(TokenService tokenService, IAtlasDbContext dbContext) =>
            (_tokenService, _dbContext) = (tokenService, dbContext);

        private static bool IsCorrectPassword(User user, string password)
        {
            return user.PasswordHash == Sha256Crypto.GetHash(user.Salt + password);
        }

        /// <summary>
        /// Change password (for all authorized)
        /// </summary>
        [HttpPut("password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePassword,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == UserId, cancellationToken);

            if (user == null || !IsCorrectPassword(user, changePassword.OldPassword))
            {
                return BadRequest();
            }

            user.Salt         = GenerateSalt();
            user.PasswordHash = Sha256Crypto.GetHash(user.Salt + changePassword.NewPassword);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Register client (for all)
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthToken>> RegisterAsync([FromBody] RegisterDto registerDto,
            CancellationToken cancellationToken)
        {
            var verification = await _dbContext.VerifyCodes.FirstOrDefaultAsync(x =>
                x.PhoneNumber == registerDto.PhoneNumber, cancellationToken);

            if (verification == null || !verification.IsVerified)
            {
                throw new NotFoundException(nameof(VerifyCode), registerDto.PhoneNumber);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Login == registerDto.PhoneNumber, cancellationToken);

            if (user != null)
            {
                return BadRequest();
            }

            var userId   = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var salt     = GenerateSalt();

            await _dbContext.Users.AddAsync(new User
            {
                Id              = userId,
                Login           = registerDto.PhoneNumber,
                FirstName       = registerDto.FirstName,
                LastName        = registerDto.LastName,
                Birthday        = registerDto.Birthday,
                CreatedAt       = DateTime.UtcNow,
                AvatarPhotoPath = "",
                IsDeleted       = false,
                Salt            = salt,
                PasswordHash    = Sha256Crypto.GetHash(salt + registerDto.Password)
            });

            await _dbContext.Clients.AddAsync(new Client
            {
                Id                          = clientId,
                UserId                      = userId,
                SelfieWithPassportPhotoPath = "",
                Balance                     = 0,
                IsDeleted                   = false,
                IsPassportVerified          = false,
                PhoneNumber                 = registerDto.PhoneNumber,
                PassportPhotoPath           = "",
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            var token = _tokenService.GenerateToken(new Claim[]
            {
                new Claim(TokenClaims.UserId, userId.ToString()),
                new Claim(TokenClaims.ClientId, clientId.ToString())
            });

            return Ok(token);
        }

        /// <summary>
        /// Auth user (for all)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthToken>> SignInAsync([FromBody] SignInDto signIn)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Login == signIn.Login);

            if (user == null || !IsCorrectPassword(user, signIn.Password))
            {
                throw new NotFoundException(nameof(User), signIn.Login);
            }

            var claims = new List<Claim>();

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

            var recruiter = await _dbContext.HeadRecruiters.FirstOrDefaultAsync(x =>
                x.UserId == user.Id);

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

            var token = _tokenService.GenerateToken(claims.ToArray());
            return Ok(token);
        }

        private static string GenerateSalt()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
