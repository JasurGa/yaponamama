namespace Atlas.Domain
{
    public class ReceiptItem
    {
        public ulong Id { get; set; }

        public ulong ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

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

        public string ComissionInfoTIN { get; set; }

        public string ComissionInfoPINFL { get; set; }
    }
}