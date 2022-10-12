using System;
using Atlas.Eskiz.Abstractions;
using Atlas.Identity.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Atlas.Identity.Services
{
    public class SmsService
    {
        private readonly IEskizClient _eskizClient;

        public SmsService(IEskizClient eskizClient)
        {
            _eskizClient = eskizClient;
            _eskizClient.AuthorizeAsync();
        }

        public bool SendSms(string toPhoneNumber, string body)
        {
            _eskizClient.SendAsync(toPhoneNumber, body);
            return true;
        }
    }
}
