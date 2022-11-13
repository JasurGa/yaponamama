using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList
{
    public class GetPromoCategoryListQuery : IRequest<PromoCategoryListVm>
    {
        public bool ShowDeleted { get; set; }
    }
}

