using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Models
{
    public class InnerItemDetailsVm
    {
        public string title { get; set; }

        public long price { get; set; }

        public long count { get; set; }

        public string code { get; set; }

        public long units { get; set; }

        public long vat_percent { get; set; }

        public string package_code { get; set; }
    }
}
