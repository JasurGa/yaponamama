﻿using System;
using System.Reactive;

namespace Atlas.Payme.MerchantApi.Models
{
    public class PerformTransactionResult
    {
        public string Transaction { get; set; }

        public long PerformTime { get; set; }

        public int State { get; set; }
    }
}

