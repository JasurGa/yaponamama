using System;
using System.Collections.Generic;
using System.Linq;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class StoreListVm
    {
        public IList<StoreLookupDto> Stores { get; set; }
    }
}
