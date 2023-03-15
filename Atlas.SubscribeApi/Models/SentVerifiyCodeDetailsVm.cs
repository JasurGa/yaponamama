using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Models
{
    public class SentVerifiyCodeDetailsVm
    {
        public bool sent { get; set; }

        public string phone { get; set; }

        public int wait { get; set; }
    }
}
