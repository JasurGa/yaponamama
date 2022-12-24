using MediatR;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class GetCategoryAndGoodListByMainCategoryQuery : IRequest<CategoryAndGoodListVm>
    {
        public Guid MainCategoryId { get; set; }

        public int GoodListSize { get; set; }
    }
}
