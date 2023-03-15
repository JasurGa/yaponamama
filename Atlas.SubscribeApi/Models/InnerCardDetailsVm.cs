using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Models
{
    public class InnerCardDetailsVm
    {
        public string number { get; set; }

        public string expire { get; set; }

        public string token { get; set; }

        public bool recurrent { get; set; }

        public bool verify { get; set; }
    }
}
