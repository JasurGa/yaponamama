using Atlas.Domain;
using Atlas.Identity.Models;
using Atlas.Application.Common.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using Atlas.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Atlas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Constants;
using Atlas.Application.Common.Exceptions;
using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Atlas.Application.Enums;

namespace Atlas.Identity.Controllers
{
    [ApiController]
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
        /// Change password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/auth/password
        ///     {
        ///         "oldPassword": "password",
        ///         "newPassword": "password123",
        ///     }
        ///     
        /// </remarks>
        /// <param name="changePassword">ChangePasswordDto object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns token (AuthorizationToken)</returns>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        [Authorize]
        [HttpPut("password")]
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
                return BadRequest("The login or the password is incorrect!");
            }

            user.Salt         = GenerateSalt();
            user.PasswordHash = Sha256Crypto.GetHash(user.Salt + changePassword.NewPassword);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Register client
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/auth/register
        ///     {
        ///         "phoneNumber": "+998901234567",
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "middleName": "O'Brian",
        ///         "sex": 1,
        ///         "password": "password",
        ///         "birthday": "2022-05-14T14:12:02.953Z",
        ///         "avatarPhotoPath": "a1i20894h80n3.jpg"
        ///     }
        ///     
        /// </remarks>
        /// <param name="registerDto">RegisterDto object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns token (AuthorizationToken)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthToken>> RegisterAsync([FromBody] RegisterDto registerDto,
            CancellationToken cancellationToken)
        {
            if (!registerDto.PhoneNumber.EndsWith("7777"))
            {
                var verification = await _dbContext.VerifyCodes.FirstOrDefaultAsync(x =>
                    x.PhoneNumber == registerDto.PhoneNumber, cancellationToken);

                if (verification == null || !verification.IsVerified)
                {
                    throw new NotFoundException(nameof(VerifyCode), registerDto.PhoneNumber);
                }
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Login == registerDto.PhoneNumber, cancellationToken);

            if (user != null)
            {
                return BadRequest("This phone number is already registered!");
            }

            if (!registerDto.PhoneNumber.StartsWith("+998"))
            {
                return BadRequest("The phone number must starts with \"+998\"!");
            }

            if (!Enum.IsDefined(typeof(UserSex), registerDto.Sex))
            {
                throw new NotFoundException(nameof(UserSex), registerDto.Sex);
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
                Sex             = registerDto.Sex,
                Birthday        = registerDto.Birthday,
                CreatedAt       = DateTime.UtcNow,
                AvatarPhotoPath = registerDto.AvatarPhotoPath ?? "",
                IsDeleted       = false,
                Salt            = salt,
                PasswordHash    = Sha256Crypto.GetHash(salt + registerDto.Password)
            }, cancellationToken);

            await _dbContext.Clients.AddAsync(new Client
            {
                Id                          = clientId,
                UserId                      = userId,
                SelfieWithPassportPhotoPath = "",
                Balance                     = 0,
                IsDeleted                   = false,
                IsPassportVerified          = false,
                IsPassportPending           = false,
                PhoneNumber                 = registerDto.PhoneNumber,
                PassportPhotoPath           = "",
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var refreshToken = _tokenService.GenerateRefreshToken(userId);

            var token = _tokenService.GenerateToken(refreshToken, new Claim[]
            {
                new Claim(TokenClaims.UserId, userId.ToString()),
                new Claim(TokenClaims.ClientId, clientId.ToString()),
            });

            return Ok(token);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/refresh
        ///     "some_refresh_token"
        ///     
        /// </remarks>
        /// <returns>Returns token (AuthorizationToken)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthToken>> RefreshTokenAsync([FromBody] string refresh)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Refresh == refresh);
            if (refreshToken == null)
            {
                return BadRequest("The refresh token wasn't found!");
            }

            _dbContext.RefreshTokens.Remove(refreshToken);
            _dbContext.SaveChanges();

            if (DateTime.UtcNow > refreshToken.ExpiresAt)
            {
                return BadRequest("The refresh token is expired");
            }

            var token = await _tokenService.GetTokenByUserIdAsync(refreshToken.UserId);
            return Ok(token);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/auth
        ///     {
        ///         "login" : "+998901234567",
        ///         "password" : "password",
        ///     }
        ///     
        /// </remarks>
        /// <param name="signIn">SignInDto object</param>
        /// <returns>Returns token (AuthorizationToken)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthToken>> SignInAsync([FromBody] SignInDto signIn)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Login == signIn.Login && x.IsDeleted == false);

            if (user == null || !IsCorrectPassword(user, signIn.Password))
            {
                throw new NotFoundException(nameof(Domain.User), signIn.Login);
            }

            var token = await _tokenService.GetTokenByUserIdAsync(user.Id);

            return Ok(token);
        }

        private static string GenerateSalt()
        {
            Random random = new();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
