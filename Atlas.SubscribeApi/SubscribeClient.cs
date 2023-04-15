using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Models;
using Atlas.SubscribeApi.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Atlas.SubscribeApi
{
    public class SubscribeClient : ISubscribeClient
    {
        private readonly SubscribeSettings _subscribeSettings;

        public SubscribeClient(IOptions<SubscribeSettings> subscribeSettings)
        {
            _subscribeSettings = subscribeSettings.Value;
        }

        public JToken? InvokeMethod(string method, object parameters)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_subscribeSettings.Url);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            webRequest.Headers.Add("X-Auth", _subscribeSettings.AuthToken);

            var joe = new JObject
            {
                ["jsonrpc"] = "1.0",
                ["id"] = "1",
                ["method"] = method,
                ["params"] = JObject.FromObject(parameters)
            };

            var s = JsonConvert.SerializeObject(joe);
            var byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using var dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            //catch
            //{
            //    throw;
            //}
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            try
            {
                using var webResponse = webRequest.GetResponse();
                using var str = webResponse.GetResponseStream();
                using var sr = new StreamReader(str);
                var response = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                return response?["result"];
            }
            catch (WebException webex)
            {
                using var str = webex?.Response?.GetResponseStream();
                //if (str is null)
                //{
                //    throw;
                //}
                if (str is null)
                {
                    throw new ArgumentNullException(nameof(str), "STR is null");
                }

                using var sr = new StreamReader(str);
                var response = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                return response?["result"];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CardDetailsVm? CardsCreate(CardsShortLookupDto card, AccountLookupDto account, bool save, string? customer)
        {
            var response = InvokeMethod("cards.create", new
            {
                card,
                account,
                save,
                customer
            });

            return response?.ToObject<CardDetailsVm>();
        }

        public SentVerifiyCodeDetailsVm? CardsGetVerifyCode(string token)
        {
            var response = InvokeMethod("cards.get_verify_code", new
            {
                token
            });

            return response?.ToObject<SentVerifiyCodeDetailsVm>();
        }

        public CardDetailsVm? CardsVerify(string token, string code)
        {
            var response = InvokeMethod("cards.verify", new
            {
                token,
                code
            });

            return response?.ToObject<CardDetailsVm>();
        }

        public CardDetailsVm? CardsCheck(string token)
        {
            var response = InvokeMethod("cards.check", new
            {
                token
            });

            return response?.ToObject<CardDetailsVm>();
        }

        public SuccessDetailsVm? CardsRemove(string token)
        {
            var response = InvokeMethod("cards.remove", new
            {
                token
            });

            return response?.ToObject<SuccessDetailsVm>();
        }

        public ReceiptDetailsVm? ReceiptsCreate(long amount, AccountLookupDto account, string description, DetailLookupDto detail)
        {
            var response = InvokeMethod("receipts.create", new
            {
                amount,
                account,
                description,
                detail
            });

            return response?.ToObject<ReceiptDetailsVm>();
        }

        public PayReceiptDetailsVm? ReceiptsPay(string id, string token, PayerLookupDto payer)
        {
            var response = InvokeMethod("receipts.pay", new
            {
                id,
                token,
                payer
            });

            return response?.ToObject<PayReceiptDetailsVm>();
        }

        public SuccessDetailsVm? ReceiptsSend(string id, string phone)
        {
            var response = InvokeMethod("receipts.send", new
            {
                id,
                phone
            });

            return response?.ToObject<SuccessDetailsVm>();
        }

        public PayReceiptDetailsVm? ReceiptsCancel(string id)
        {
            var response = InvokeMethod("receipts.cancel", new
            {
                id,
            });

            return response?.ToObject<PayReceiptDetailsVm>();
        }

        public StateDetailsVm? ReceiptsCheck(string id)
        {
            var response = InvokeMethod("receipts.check", new
            {
                id,
            });

            return response?.ToObject<StateDetailsVm>();
        }

        public PayReceiptDetailsVm? ReceiptsGet(string id)
        {
            var response = InvokeMethod("receipts.get", new
            {
                id,
            });

            return response?.ToObject<PayReceiptDetailsVm>();
        }

        public List<InnerPayReceiptDetailsVm>? ReceiptsGetAll(long count, long from, long to, long offset)
        {
            var response = InvokeMethod("receipts.get_all", new
            {
                count,
                from,
                to,
                offset
            });

            return response?.ToObject<List<InnerPayReceiptDetailsVm>>();
        }

        public SuccessDetailsVm? ReceiptsSetFiscalData(string id, FiscalDataLookupDto fiscal_data)
        {
            var response = InvokeMethod("receipts.set_fiscal_data", new
            {
                id,
                fiscal_data
            });

            return response?.ToObject<SuccessDetailsVm>();
        }
    }
}

