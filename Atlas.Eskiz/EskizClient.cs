using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Atlas.Eskiz.Abstractions;
using Atlas.Eskiz.Models;
using Atlas.Eskiz.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Atlas.Eskiz
{
    public class EskizClient : IEskizClient
    {
        private string _token = null;

        private readonly EskizSettings _eskizSettings;

        private static readonly HttpClient client = new HttpClient();

        public EskizClient(IOptions<EskizSettings> eskizSettings)
        {
            _eskizSettings = eskizSettings.Value;
        }

        public async Task AuthorizeAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", _eskizSettings.Email },
                { "password", _eskizSettings.Password }
            });

            Console.WriteLine("Eskiz.Email: ", _eskizSettings.Email);
            Console.WriteLine("Eskiz.Password: ", _eskizSettings.Password);

            HttpResponseMessage response;
            string responseString;

            try
            {
                response = await client.PostAsync("https://notify.eskiz.uz/api/auth/login", content);
                responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Eskiz.Auth", responseString);
            }
            catch (HttpRequestException)
            {
                throw new Exception("Can't send auth request!");
            }

            try
            {
                var authResponse = JsonConvert.DeserializeObject<EskizAuthResponse>(responseString);
                _token = authResponse.data.token;
            }
            catch (JsonException)
            {
                throw new Exception("Can't authorize!");
            }
        }

        public async Task RefreshTokenAsync()
        {
            if (_token == null)
            {
                throw new Exception("Authorize first!");
            }

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Patch, "https://notify.eskiz.uz/api/auth/refresh"))
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.SendAsync(requestMessage);
                Console.WriteLine("Eskiz.Refresh", await response.Content.ReadAsStringAsync());
            }
        }

        public async Task SendAsync(string mobilePhone, string message)
        {
            if (_token == null)
            {
                throw new Exception("Authorize first!");
            }

            mobilePhone = mobilePhone.Replace("+", "");
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://notify.eskiz.uz/api/message/sms/send"))
            {
                var values = new Dictionary<string, string>
                {
                    { "mobile_phone", mobilePhone },
                    { "message", message },
                    { "from", _eskizSettings.FromNumber },
                };

                requestMessage.Content = new FormUrlEncodedContent(values);
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.SendAsync(requestMessage);
                Console.WriteLine("Eskiz.Send", await response.Content.ReadAsStringAsync());
            }
        }
    }
}

