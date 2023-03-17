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
        public static void Main(string[] args)
        {
            var id       = "634006530c7921dcc8394821";
            var password = "UXTVWZe4Q@XevDbhRjj&M%zq1KCKPZoKN?OK";

            var subscribeApiClientFrontend = new SubscribeClient(Options.Create<SubscribeSettings>(new SubscribeSettings
            {
                Url       = "https://checkout.test.paycom.uz/api",
                AuthToken = $"{id}"
            }));

            var subscribeApiClientBackend = new SubscribeClient(Options.Create<SubscribeSettings>(new SubscribeSettings
            {
                Url = "https://checkout.test.paycom.uz/api",
                AuthToken = $"{id}:{password}"
            }));

            Console.WriteLine(CardsCreate(subscribeApiClientFrontend));
        }

        public static string CardsCreate(ISubscribeClient subscribeApiClient)
        {
            var result = subscribeApiClient.CardsCreate(new CardsShortLookupDto
            {
                number = "8600495473316478",
                expire = "0399"
            },
            new AccountLookupDto
            {
            },
            true,
            null);

            return JsonConvert.SerializeObject(result);
        }
    }
}