using System;
namespace Atlas.Payme.MerchantApi.Models
{
    public class GetStatementResultLookupDto
    {
        public string Id { get; set; }

        public long Time { get; set; }

        public int Amount { get; set; }

        public AccountDto Account { get; set; }

        public long CreateTime { get; set; }

        public long PerformTime { get; set; }

        public long CancelTime { get; set; }

        public string Transaction { get; set; }

        public int State { get; set; }

        public int? Reason { get; set; }

        public int? Receivers { get; set; }
    }
}

