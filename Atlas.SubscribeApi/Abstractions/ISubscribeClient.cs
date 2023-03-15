using Atlas.SubscribeApi.Models;
using System;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Abstractions
{
    public interface ISubscribeClient
    {
        Task<CardDetailsVm> CardsCreateAsync(CardsShortLookupDto card, AccountLookupDto account, bool save, string customer);

        Task<SentVerifiyCodeDetailsVm> CardsGetVerifyCodeAsync(string token);

        Task<CardDetailsVm> CardsVerifyAsync(string token, string code);

        Task<CardDetailsVm> CardsCheckAsync(string token);

        Task<SuccessDetailsVm> CardsRemoveAsync(string token);

        Task<ReceiptDetailsVm> ReceiptsCreateAsync(long amount, AccountLookupDto account, string description, DetailLookupDto detail);

        Task<PayReceiptDetailsVm> ReceiptsPayAsync(string id, string token, PayerLookupDto payer);

        Task<SuccessDetailsVm> ReceiptsSendAsync(string id, string phone);

        Task<PayReceiptDetailsVm> ReceiptsCancelAsync(string id);

        Task<StateDetailsVm> ReceiptsCheckAsync(string id);

        Task<PayReceiptDetailsVm> ReceiptsGetAsync(string id);

        Task<List<InnerPayReceiptDetailsVm>> ReceiptsGetAllAsync(long count, long from, long to, long offset);

        Task<SuccessDetailsVm> ReceiptsSetFiscalDataAsync(string id, FiscalDataLookupDto fiscalData);
    }
}

