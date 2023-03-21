using Atlas.SubscribeApi;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Models;
using Atlas.SubscribeApi.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Atlas.TestApp 
{
    public class Program
    {
        public static void Main()
        {
            var id       = "641429f38e437c587b7b4326";
            var password = "";

            var subscribeApiClient = new SubscribeClient(Options.Create(new SubscribeSettings
            {
                Url       = "https://checkout.test.paycom.uz/api",
                AuthToken = $"{id}"
            }));

            var cardsCreate = subscribeApiClient.CardsCreate(new CardsShortLookupDto
            {
                number = "8600495473316478",
                expire = "0399"
            },
            new AccountLookupDto { }, true, null);

            var cardsGetVerifyCode = subscribeApiClient.CardsGetVerifyCode(cardsCreate.card.token);
            var cardsVerify = subscribeApiClient.CardsVerify(cardsCreate.card.token, "666666");

            Console.WriteLine(JsonConvert.SerializeObject(cardsCreate));
            Console.WriteLine(JsonConvert.SerializeObject(cardsGetVerifyCode));
            Console.WriteLine(JsonConvert.SerializeObject(cardsVerify));

            subscribeApiClient = new SubscribeClient(Options.Create(new SubscribeSettings
            {
                Url       = "https://checkout.test.paycom.uz/api",
                AuthToken = $"{id}:{password}"
            }));

        }
    }
}