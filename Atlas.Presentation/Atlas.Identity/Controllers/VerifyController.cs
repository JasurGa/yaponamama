using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Identity.Models;
using Atlas.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Identity.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VerifyController : ControllerBase
    {
        private readonly IAtlasDbContext _dbContext;

        private readonly SmsService _smsService;

        private readonly TokenService _tokenService;

        public VerifyController(IAtlasDbContext dbContext, SmsService smsService, TokenService tokenService) =>
            (_dbContext, _smsService, _tokenService) = (dbContext, smsService, tokenService);

        /// <summary>
        /// Send validation code
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> SendSmsAsync([FromBody] SendVerifySmsDto sendVerifySmsDto,
            CancellationToken cancellationToken)
        {
            var oldVerificationCode = await _dbContext.VerifyCodes.FirstOrDefaultAsync(x =>
                x.PhoneNumber == sendVerifySmsDto.PhoneNumber, cancellationToken);

            if (oldVerificationCode != null)
            {
                // if (oldVerificationCode.IsVerified)
                // {
                //     return Accepted();
                // }

                _dbContext.VerifyCodes.Remove(oldVerificationCode);
            }

            var newCode = GenerateVerificationCode();
            _smsService.SendSms(sendVerifySmsDto.PhoneNumber, newCode);
            _dbContext.VerifyCodes.Add(new Domain.VerifyCode
            {
                PhoneNumber      = sendVerifySmsDto.PhoneNumber,
                ExpiresAt        = DateTime.UtcNow.AddMinutes(10),
                IsVerified       = false,
                VerificationCode = newCode
            });
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Verify phone number
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyPhoneAsync([FromBody] VerifyPhoneDto verifyPhoneDto,
            CancellationToken cancellationToken)
        {
            var verificationCode = await _dbContext.VerifyCodes
                .FirstOrDefaultAsync(x => x.PhoneNumber == verifyPhoneDto.PhoneNumber &&
                    x.VerificationCode == verifyPhoneDto.VerificationCode, cancellationToken);

            if (verificationCode == null || verificationCode.ExpiresAt < DateTime.UtcNow)
            {
                return NotFound();
            }

            var userId = Guid.Empty;

            if (_dbContext.VerifyCodes.FirstOrDefault(x => x.IsVerified &&
                x.PhoneNumber == verifyPhoneDto.PhoneNumber) == null)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Login == verifyPhoneDto.PhoneNumber);
                if (user != null)
                    userId = user.Id;
            }

            verificationCode.IsVerified = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (!userId.Equals(Guid.Empty))
            {
                return Accepted(_tokenService.GetTokenByUserIdAsync(userId));
            }

            return Ok();
        }

        private static string GenerateVerificationCode()
        {
            Random random = new Random();
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
