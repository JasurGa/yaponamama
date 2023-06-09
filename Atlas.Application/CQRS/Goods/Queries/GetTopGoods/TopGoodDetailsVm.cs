﻿using System;
using System.Collections.Generic;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;

namespace Atlas.Application.CQRS.Goods.Queries.GetTopGoods
{
    public class TopGoodDetailsVm
    {
        public CategoryLookupDto Category { get; set; }

        public IList<GoodLookupDto> Goods { get; set; }
    }
}
