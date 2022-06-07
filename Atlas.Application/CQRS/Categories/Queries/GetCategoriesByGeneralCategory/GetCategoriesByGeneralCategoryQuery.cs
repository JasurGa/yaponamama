using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoriesByGeneralCategory
{
    public class GetCategoriesByGeneralCategoryQuery
        : IRequest<CategoryListVm>
    {
        public bool ShowDeleted { get; set; }

        public Guid GeneralCategoryId { get; set; }
    }
}
