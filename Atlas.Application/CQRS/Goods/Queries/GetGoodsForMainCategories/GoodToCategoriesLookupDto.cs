using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories
{
    public class GoodToCategoriesLookupDto
    {
        public Guid GoodId { get; set; }

        public List<Guid> CategoryIds { get; set; }
    }
}

