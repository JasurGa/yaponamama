using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<CategoryListVm>
    {

    }
}
