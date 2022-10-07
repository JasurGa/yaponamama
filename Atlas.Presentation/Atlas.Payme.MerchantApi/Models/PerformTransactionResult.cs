using System;
using System.Reactive;

namespace Atlas.Payme.MerchantApi.Models
{
    public class PerformTransactionResult
    {
        public string Transaction { get; set; }

        public long Timestamp { get; set; }

        public int State { get; set; }
    }
}

