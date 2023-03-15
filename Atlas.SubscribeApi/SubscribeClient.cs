using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Models;
using Atlas.SubscribeApi.Settings;
using Microsoft.Extensions.Options;

namespace Atlas.SubscribeApi
{
    public class SubscribeClient : ISubscribeClient
    {
        private readonly HttpClient _client = new HttpClient();

        private readonly SubscribeSettings _subscribeSettings;

        public SubscribeClient(IOptions<SubscribeSettings> subscribeSettings)
        {
            _subscribeSettings = subscribeSettings.Value;
        }

        public async Task<CardDetailsVm> CardsCreateAsync(CardsShortLookupDto card, AccountLookupDto account, bool save, string customer)
        {
            throw new NotImplementedException();
        }

        public async Task<SentVerifiyCodeDetailsVm> CardsGetVerifyCodeAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CardDetailsVm> CardsVerifyAsync(string token, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<CardDetailsVm> CardsCheckAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<SuccessDetailsVm> CardsRemoveAsync(string token)
        {
            throw new NotImplementedException();
        }
        
        public async Task<ReceiptDetailsVm> ReceiptsCreateAsync(long amount, AccountLookupDto account, string description, DetailLookupDto detail)
        {
            throw new NotImplementedException();
        }

        public async Task<PayReceiptDetailsVm> ReceiptsPayAsync(string id, string token, PayerLookupDto payer)
        {
            throw new NotImplementedException();
        }

        public async Task<SuccessDetailsVm> ReceiptsSendAsync(string id, string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<PayReceiptDetailsVm> ReceiptsCancelAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<StateDetailsVm> ReceiptsCheckAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PayReceiptDetailsVm> ReceiptsGetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InnerPayReceiptDetailsVm>> ReceiptsGetAllAsync(long count, long from, long to, long offset)
        {
            throw new NotImplementedException();
        }

        public async Task<SuccessDetailsVm> ReceiptsSetFiscalDataAsync(string id, FiscalDataLookupDto fiscalData)
        {
            throw new NotImplementedException();
        }
    }
}

