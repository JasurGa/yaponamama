using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Domain
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

        public ICollection<ReceiptItem> Items { get; set; }

        public float LocationLatitude { get; set; }

        public float LocationtLongitude { get; set; }

        public string TaxiInfoTIN { get; set; }

        public string TaxiInfoPINFL { get; set; }

        public string TaxiInfoCarNumber { get; set; }

        public string RefundInfoTerminalID { get; set; }

        public string RefundInfoReceiptId { get; set; }

        public string RefundInfoDateTime { get; set; }

        public string RefundInfoFiscalSign { get; set; }

        public string ExtraInfoPhoneNumber { get; set; }

        public string ExtraInfoOther { get; set; }
    }
}
