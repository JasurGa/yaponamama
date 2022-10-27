using System;
namespace Atlas.Payme.MerchantApi.Models
{
    public class CheckPerformTransactionResult
    {
        public bool Allow { get; set; }

        public DetailsLookupDto Detail { get; set; }
    }
}

