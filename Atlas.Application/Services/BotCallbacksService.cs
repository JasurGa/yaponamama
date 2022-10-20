using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Atlas.Application.Services
{
    public class BotCallbacksService : IBotCallbacksService
    {
        private static readonly HttpClient client = new HttpClient();

        public string GetUrlByIsDev(bool isDevVersionBot)
        {
            return !isDevVersionBot ? "https://botapi.oqot.uz" : "https://botapidev.oqot.uz";
        }

        public async Task<string> UpdateStatusAsync(int telegramUserId, bool isDevVersionBot, Guid orderId, int status)
        {
            HttpResponseMessage response;
            string responseString;

            try
            {
                response = await client.GetAsync($"{GetUrlByIsDev(isDevVersionBot)}/callback/change_status"
                                               + $"?user_id={telegramUserId}&order_id={orderId.ToString()}"
                                               + $"&status={status}");
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                throw new Exception("Exception: Can't send change_status request!");
            }

            return responseString;
        }
    }
}

