using Atlas.SubscribeApi.Models;
using System;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Abstractions
{
    public interface ISubscribeClient
    {
        CardDetailsVm? CardsCreate(CardsShortLookupDto card, AccountLookupDto account, bool save, string? customer);

        SentVerifiyCodeDetailsVm? CardsGetVerifyCode(string? token);

        CardDetailsVm? CardsVerify(string? token, string? code);

        CardDetailsVm? CardsCheck(string? token);

        SuccessDetailsVm? CardsRemove(string? token);

        ReceiptDetailsVm? ReceiptsCreate(long amount, AccountLookupDto account, string? description, DetailLookupDto detail);

        PayReceiptDetailsVm? ReceiptsPay(string? id, string? token, PayerLookupDto payer);

        SuccessDetailsVm? ReceiptsSend(string? id, string? phone);

        PayReceiptDetailsVm? ReceiptsCancel(string? id);

        StateDetailsVm? ReceiptsCheck(string? id);

        PayReceiptDetailsVm? ReceiptsGet(string? id);

        List<InnerPayReceiptDetailsVm>? ReceiptsGetAll(long count, long from, long to, long offset);

        SuccessDetailsVm? ReceiptsSetFiscalData(string? id, FiscalDataLookupDto fiscalData);
    }
}

