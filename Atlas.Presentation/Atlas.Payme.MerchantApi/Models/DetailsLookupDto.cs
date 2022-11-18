using System.Collections.Generic;

namespace Atlas.Payme.MerchantApi.Models
{
    public class DetailsLookupDto
    {
        public ShippingLookupDto Shipping { get; set; }

        public IEnumerable<ItemLookupDto> Items { get; set; }
    }
}