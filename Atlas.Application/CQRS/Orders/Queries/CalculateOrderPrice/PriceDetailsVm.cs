namespace Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice
{
    public class PriceDetailsVm
    {
        public float ShippingPrice { get; set; }

        public float SellingPrice { get; set; }

        public float TotalPrice
        {
            get
            {
                return ShippingPrice + SellingPrice;
            }
        }
    }
}