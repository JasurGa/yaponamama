using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Models
{
    public class InnerReceiptDetailsVm
    {
        public string _id { get; set; }

        public BigInteger create_time { get; set; }

        public BigInteger pay_time { get; set; }

        public BigInteger cancel_time { get; set; }

        public long state { get; set; }

        public long type { get; set; }

        public bool external { get; set; }

        public long operation { get; set; }

        public object? category { get; set; }

        public InnerErrorDetailsVm error { get; set; }

        public string description { get; set; }

        public InnerDetailDetailsVm detail { get; set; }

        public long amount { get; set; }

        public long currency { get; set; }

        public long comission { get; set; }

        public List<AccountLookupDto> account { get; set; }
    }
}
