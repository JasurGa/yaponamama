namespace Atlas.SubscribeApi.Models
{
    public class DetailLookupDto
    {
        public long receipt_type { get; set; }

        public InnerShippingDetailsVm shipping { get; set; }

        public List<InnerItemDetailsVm> items { get; set; }
    }
}