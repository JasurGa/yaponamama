using System;
using System.Collections.Generic;

namespace Atlas.Payme.MerchantApi.Models
{
    public class GetStatementResult
    {
        public List<GetStatementResultLookupDto> Transactions { get; set; }
    }
}

