using MediatR;
using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQuery : IRequest<CategoryToGoodListVm>
    {
        public Guid GoodId { get; set; }
    }
}
