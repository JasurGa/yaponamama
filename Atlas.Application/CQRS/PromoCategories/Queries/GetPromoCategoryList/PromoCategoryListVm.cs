using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList
{
    public class PromoCategoryListVm
    {
        public IList<PromoCategoryLookupDto> PromoCategories { get; set; }
    }
}

