namespace Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice
{
    public class PriceDetailsVm
    {
        public long ShippingPrice { get; set; }

        public long SellingPrice { get; set; }

        public long SellingPriceDiscount { get; set; }

        public long ShippingPriceDiscount { get; set; }

        public long TotalPrice
        {
            get
            {
                return ShippingPrice + SellingPrice - (SellingPriceDiscount + ShippingPriceDiscount);
            }
        }
    }
}