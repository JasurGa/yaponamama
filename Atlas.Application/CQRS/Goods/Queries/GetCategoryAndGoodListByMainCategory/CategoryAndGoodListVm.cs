using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class CategoryAndGoodListVm
    {                                
        public List<CategoryLookupDto> Categories { get; set; }
    }
}
