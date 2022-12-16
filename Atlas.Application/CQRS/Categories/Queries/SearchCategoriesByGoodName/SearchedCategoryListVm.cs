using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchedCategoryListVm
    {
        public ICollection<SearchedCategoryLookupDto> Categories { get; set; }
    }
}

