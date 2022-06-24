using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Atlas.Identity.Helpers;
using Atlas.Identity.Models;
using Atlas.Identity.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Identity.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ForgotController : ControllerBase
    {
        private readonly SmsService _smsService;

        private readonly IAtlasDbContext _dbContext;

        public ForgotController(SmsService smsService, IAtlasDbContext dbContext) =>
            (_smsService, _dbContext) = (smsService, dbContext);

        /// <summary>
        /// Send validation code
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> SendSmsAsync([FromBody] SendForgotPasswordSmsDto sendForgotPasswordSmsDto,
            CancellationToken cancellationToken)
        {
            var oldVerificationCode = await _dbContext.ForgotPasswordCodes.FirstOrDefaultAsync(x =>
                x.PhoneNumber == sendForgotPasswordSmsDto.PhoneNumber, cancellationToken);

            if (oldVerificationCode != null)
            {
                _dbContext.ForgotPasswordCodes.Remove(oldVerificationCode);
            }

            var newCode = GenerateVerificationCode();
            _smsService.SendSms(sendForgotPasswordSmsDto.PhoneNumber, newCode);
            _dbContext.ForgotPasswordCodes.Add(new ForgotPasswordCode
            {
                PhoneNumber      = sendForgotPasswordSmsDto.PhoneNumber,
                ExpiresAt        = DateTime.UtcNow.AddMinutes(10),
                VerificationCode = newCode
            });
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Reset password
        /// </summary>
        [HttpPost("reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPasswordDto,
            CancellationToken cancellationToken)
        {
            var forgotPasswordCode = await _dbContext.ForgotPasswordCodes
                .FirstOrDefaultAsync(x => x.PhoneNumber == resetPasswordDto.PhoneNumber &&
                    x.VerificationCode == resetPasswordDto.VerificationCode,
                    cancellationToken);

            if (forgotPasswordCode == null || forgotPasswordCode.ExpiresAt < DateTime.UtcNow)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Login == resetPasswordDto.PhoneNumber);

            user.Salt         = GenerateSalt();
            user.PasswordHash = Sha256Crypto.GetHash(user.Salt + resetPasswordDto.NewPassword);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        private static string GenerateSalt()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
