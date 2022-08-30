using System;
using Atlas.Identity.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Atlas.Identity.Services
{
    public class SmsService
    {
        private readonly SmsSettings _smsSettings;

        public SmsService(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
            TwilioClient.Init(_smsSettings.AccountSid,
                _smsSettings.AuthToken);
        }

        public bool SendSms(string toPhoneNumber, string body)
        {
            var message = MessageResource.Create(
                body: body,
                //from: new Twilio.Types.PhoneNumber(_smsSettings.FromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber),
                messagingServiceSid: _smsSettings.MessagingServiceSid
            );

            return message.ErrorCode == 0;
        }
    }
}
