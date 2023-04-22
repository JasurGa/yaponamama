using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.OfdApi.Models
{
    public class Receipt
    {
        public ulong ReceiptId { get; set; }

        public ulong ReceivedCash { get; set; }

        public ulong ReceivedCard { get; set; }

        public string Time { get; set; }

        public ulong TotalVAT { get; set; }

        public byte IsRefund { get; set; }

        public byte ReceiptType { get; set; }

        public ICollection<Item> Items { get; set; }

        public Location Location { get; set; }

        public TaxiInfo TaxiInfo { get; set; }
        
        public RefundInfo RefundInfo { get; set; }

        public ExtraInfo ExtraInfo { get; set; }
    }
}
