using System.Collections.Generic;

namespace Atlas.Payme.MerchantApi.Models
{
    public class DetailsLookupDto
    {
        public IEnumerable<ItemLookupDto> Items { get; set; }
    }
}