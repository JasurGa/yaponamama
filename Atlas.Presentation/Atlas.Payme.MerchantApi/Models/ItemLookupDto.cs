namespace Atlas.Payme.MerchantApi.Models
{
    public class ItemLookupDto
    {
        public string Title { get; set; }

        public long Price { get; set; }

        public int Count { get; set; }

        public string Code { get; set; }

        public string PackageCode { get; set; }

        public int VatPercent { get; set; }
    }
}