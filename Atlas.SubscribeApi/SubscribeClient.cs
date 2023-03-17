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
            webRequest.Method      = "POST";

            webRequest.Headers.Add("X-Auth", _subscribeSettings.AuthToken);

            var joe        = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"]      = "1";
            joe["method"]  = method;
            joe["params"]  = JObject.FromObject(parameters);

            var s = JsonConvert.SerializeObject(joe);
            var byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (var dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch
            {
                throw;
            }

            WebResponse webResponse = null;
            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (var str = webResponse.GetResponseStream())
                    {
                        using (var sr = new StreamReader(str))
                        {
                            var response = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                            return response != null ? response["result"] : null;
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (var str = webex.Response.GetResponseStream())
                {
                    using (var sr = new StreamReader(str))
                    {
                        var response = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        return response != null ? response["result"] : null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CardDetailsVm? CardsCreate(CardsShortLookupDto card, AccountLookupDto account, bool save, string? customer)
        {
            var response = InvokeMethod("cards.create", new
            {
                card     = card,
                account  = account, 
                save     = save,
                customer = customer
            });

            return response != null ? response.ToObject<CardDetailsVm>() : null;
        }

        public SentVerifiyCodeDetailsVm? CardsGetVerifyCode(string token)
        {
            var response = InvokeMethod("cards.get_verify_code", new
            {
                token = token
            });

            return response != null ? response.ToObject<SentVerifiyCodeDetailsVm>() : null;
        }

        public CardDetailsVm? CardsVerify(string token, string code)
        {
            var response = InvokeMethod("cards.verify", new
            {
                token = token,
                code  = code
            });

            return response != null ? response.ToObject<CardDetailsVm>() : null;
        }

        public CardDetailsVm? CardsCheck(string token)
        {
            var response = InvokeMethod("cards.check", new
            {
                token = token
            });

            return response != null ? response.ToObject<CardDetailsVm>() : null;
        }

        public SuccessDetailsVm? CardsRemove(string token)
        {
            var response = InvokeMethod("cards.remove", new
            {
                token = token
            });

            return response != null ? response.ToObject<SuccessDetailsVm>() : null;
        }
        
        public ReceiptDetailsVm? ReceiptsCreate(long amount, AccountLookupDto account, string description, DetailLookupDto detail)
        {
            var response = InvokeMethod("receipts.create", new
            {
                amount      = amount,
                account     = account,
                description = description,
                detail      = detail
            });

            return response != null ? response.ToObject<ReceiptDetailsVm>() : null;
        }

        public PayReceiptDetailsVm? ReceiptsPay(string id, string token, PayerLookupDto payer)
        {
            var response = InvokeMethod("receipts.pay", new
            {
                id    = id,
                token = token,
                payer = payer
            });

            return response != null ? response.ToObject<PayReceiptDetailsVm>() : null;
        }

        public SuccessDetailsVm? ReceiptsSend(string id, string phone)
        {
            var response = InvokeMethod("receipts.send", new
            {
                id    = id,
                phone = phone
            });

            return response != null ? response.ToObject<SuccessDetailsVm>() : null;
        }

        public PayReceiptDetailsVm? ReceiptsCancel(string id)
        {
            var response = InvokeMethod("receipts.cancel", new
            {
                id = id,
            });

            return response != null ? response.ToObject<PayReceiptDetailsVm>() : null;
        }

        public StateDetailsVm? ReceiptsCheck(string id)
        {
            var response = InvokeMethod("receipts.check", new
            {
                id = id,
            });

            return response != null ? response.ToObject<StateDetailsVm>() : null;
        }

        public PayReceiptDetailsVm? ReceiptsGet(string id)
        {
            var response = InvokeMethod("receipts.get", new
            {
                id = id,
            });

            return response != null ? response.ToObject<PayReceiptDetailsVm>() : null;
        }

        public List<InnerPayReceiptDetailsVm>? ReceiptsGetAll(long count, long from, long to, long offset)
        {
            var response = InvokeMethod("receipts.get_all", new
            {
                count  = count,
                from   = from,
                to     = to,
                offset = offset
            });

            return response != null ? response.ToObject<List<InnerPayReceiptDetailsVm>>() : null;
        }

        public SuccessDetailsVm? ReceiptsSetFiscalData(string id, FiscalDataLookupDto fiscal_data)
        {
            var response = InvokeMethod("receipts.set_fiscal_data", new
            {
                id          = id,
                fiscal_data = fiscal_data
            });

            return response != null ? response.ToObject<SuccessDetailsVm>() : null;        
        }
    }
}

