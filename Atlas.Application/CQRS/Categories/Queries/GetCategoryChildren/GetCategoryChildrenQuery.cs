using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildren
{
    public class GetCategoryChildrenQuery : IRequest<CategoryListVm>
    {
        public Guid Id { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
