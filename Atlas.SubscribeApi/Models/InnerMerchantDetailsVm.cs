namespace Atlas.SubscribeApi.Models
{
    public class InnerMerchantDetailsVm
    {
        public string _id { get; set; }

        public string name { get; set; }

        public string organization { get; set; }

        public string address { get; set; }

        public InnerEposDetailsVm epos { get; set; }

        public long date { get; set; }

        public string logo { get; set; }

        public string type { get; set; }

        public InnerTermsDetailsVm terms { get; set; }

        public PayerShortLookupDto payer { get; set; }
    }
}