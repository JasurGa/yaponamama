using System;
namespace Atlas.Payme.MerchantApi.Models
{
    public class CancelTransactionResult
    {
        public string Transaction { get; set; }

        public long CancelTime { get; set; }

        public int State { get; set; }
    }
}

