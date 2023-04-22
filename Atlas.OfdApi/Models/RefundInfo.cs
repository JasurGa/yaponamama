using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.OfdApi.Models
{
    public class RefundInfo
    {
        public string TerminalID { get; set; }

        public string ReceiptId { get; set; }

        public string DateTime { get; set; }

        public string FiscalSign { get; set; }
    }
}
