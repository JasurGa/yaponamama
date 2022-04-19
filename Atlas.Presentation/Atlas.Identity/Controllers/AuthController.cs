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

namespace Atlas.Identity.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAtlasDbContext _dbContext;

        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService, IAtlasDbContext dbContext) =>
            (_tokenService, _dbContext) = (tokenService, dbContext);

        private static bool IsCorrectPassword(User user, string password)
        {
            return user.PasswordHash == Sha256Crypto.GetHash(user.Salt + password);
        }

        /// <summary>
        /// Auth user (for all)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AuthToken), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInDto signIn)
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
    }
}
