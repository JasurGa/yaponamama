using Atlas.SubscribeApi;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Enums;
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
            var id       = "63eb50ca32a352d85bada8d0";
            var password = "FQP394DJ4fqHQnp6Wnntmu8dw5w7o2B3pcLB";

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

            var receipt = subscribeApiClient.ReceiptsCreate(100000, new AccountLookupDto { 
                    order_id = Guid.NewGuid(),
                },
                "OQ-OT: Оплата заказа", new DetailLookupDto
                {
                    receipt_type = (int)ReceiptTypes.Fiscal,
                    shipping = new InnerShippingDetailsVm
                    {
                        title = "Доставка",
                        price = 50000
                    },
                    items = new List<InnerItemDetailsVm> { 
                        new InnerItemDetailsVm 
                        {
                            discount     = 0,
                            title        = "Тестовый товар",
                            price        = 50000,
                            count        = 1,
                            code         = "00702001001000001",
                            units        = 241092,
                            vat_percent  = 15,
                            package_code = "123456"
                        } 
                    }
                });

            var payment = subscribeApiClient.ReceiptsPay(receipt.receipt._id, cardsCreate.card.token,
                null);

            var sent = subscribeApiClient.ReceiptsSend(receipt.receipt._id, "998901234567");

            Console.WriteLine(JsonConvert.SerializeObject(receipt));
            Console.WriteLine(JsonConvert.SerializeObject(payment));
            Console.WriteLine(JsonConvert.SerializeObject(sent));
        }
    }
}