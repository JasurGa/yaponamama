using System.Collections.Generic;

namespace Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList
{
    public class MainCategoryListVm
    {
        public IList<MainCategoryLookupDto> Maincategories { get; set; }
    }
}
