using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using MediatR;
using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQuery : IRequest<CategoryListVm>
    {
        public Guid GoodId { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
