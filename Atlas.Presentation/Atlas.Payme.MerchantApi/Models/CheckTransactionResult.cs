using System;
using System.Drawing;
using System.Reactive;

namespace Atlas.Payme.MerchantApi.Models
{
    public class CheckTransactionResult
    {
        public long CreateTime { get; set; }

        public long PerformTime { get; set; }

        public long CancelTime { get; set; }

        public string Transaction { get; set; }

        public int State { get; set; }

        public int? Reason { get; set; }
    }
}

