namespace Atlas.SubscribeApi.Models
{
    public class InnerDetailDetailsVm
    {
        public InnerDiscountDetailsVm discount { get; set; }

        public InnerShippingDetailsVm shipping { get; set; }

        public List<InnerItemDetailsVm> items { get; set; }
    }
}