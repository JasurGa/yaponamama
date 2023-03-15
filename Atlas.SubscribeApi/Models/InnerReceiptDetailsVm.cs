using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Models
{
    public class InnerReceiptDetailsVm
    {
        public string _id { get; set; }

        public long create_time { get; set; }

        public long pay_time { get; set; }

        public long cancel_time { get; set; }

        public long state { get; set; }

        public long type { get; set; }

        public bool external { get; set; }

        public long operation { get; set; }

        public InnerCategoryDetailsVm category { get; set; }

        public InnerErrorDetailsVm error { get; set; }

        public string description { get; set; }

        public InnerDetailDetailsVm detail { get; set; }

        public long amount { get; set; }

        public long currency { get; set; }

        public long comission { get; set; }

        public InnerAccountDetailsVm account { get; set; }
    }
}
