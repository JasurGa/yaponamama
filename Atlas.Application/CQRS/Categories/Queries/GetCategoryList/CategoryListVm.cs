using System.Collections.Generic;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryList
{
    public class CategoryListVm
    {
        public IList<CategoryLookupDto> Categories { get; set; }
    }
}
