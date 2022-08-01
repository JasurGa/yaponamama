using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;

namespace Atlas.Application.CQRS.Statistics.Queries.GetGoodsCountStatistics
{
    public class GoodsCountLookupDto
    {
        public CategoryLookupDto Category { get; set; }

        public int GoodCount { get; set; }
    }
}
