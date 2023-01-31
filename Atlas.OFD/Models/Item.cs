using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.OFD.Models
{
    public class Item
    {
        public string Name { get; set; }

        public string Barcode { get; set; }

        public string Label { get; set; }

        public string SPIC { get; set; }

        public ulong Units { get; set; }

        public string PackageCode { get; set; }

        public ulong GoodPrice { get; set; }

        public ulong Price { get; set; }

        public ulong Amount { get; set; }

        public ulong VAT { get; set; }

        public byte VATPercent { get; set; }

        public ulong Discount { get; set; }

        public ulong Other { get; set; }
        
        public ComissionInfo ComissionInfo { get; set; }
    }
}
