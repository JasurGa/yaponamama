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
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/api/[controller]")]
    public class VerifyController : ControllerBase
    {
        private readonly IAtlasDbContext _dbContext;

        private readonly SmsService _smsService;

        public VerifyController(IAtlasDbContext dbContext, SmsService smsService) =>
            (_dbContext, _smsService) = (dbContext, smsService);

        /// <summary>
        /// Send validation code
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> SendSmsAsync([FromBody] SendVerifySmsDto sendVerifySmsDto,
            CancellationToken cancellationToken)
        {
            var oldVerificationCode = await _dbContext.VerifyCodes
                .FirstOrDefaultAsync(x => x.PhoneNumber == sendVerifySmsDto.PhoneNumber);

            if (oldVerificationCode != null)
            {
                if (oldVerificationCode.IsVerified)
                {
                    return Accepted();
                }

                _dbContext.VerifyCodes.Remove(oldVerificationCode);
                await _dbContext.SaveChangesAsync(cancellationToken);
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
        public async Task<IActionResult> SendSmsAsync([FromBody] VerifyPhoneDto verifyPhoneDto,
            CancellationToken cancellationToken)
        {
            var verificationCode = await _dbContext.VerifyCodes
                .FirstOrDefaultAsync(x => x.PhoneNumber == verifyPhoneDto.PhoneNumber &&
                    x.VerificationCode == verifyPhoneDto.VerificationCode);

            if (verificationCode == null || verificationCode.ExpiresAt < DateTime.UtcNow)
            {
                return NotFound();
            }

            verificationCode.IsVerified = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

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
