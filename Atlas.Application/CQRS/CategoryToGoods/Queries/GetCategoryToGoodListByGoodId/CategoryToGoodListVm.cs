using System.Collections.Generic;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class CategoryToGoodListVm
    {
        public IList<CategoryToGoodLookupDto> CategoryToGoods { get; set; }
    }
}
