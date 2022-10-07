using System;
namespace Atlas.Payme.MerchantApi.Models
{
    public class CreateTransactionResult
    {
        public long CreateTime { get; set; }

        public string Transaction { get; set; }

        public int State { get; set; }
    }
}

