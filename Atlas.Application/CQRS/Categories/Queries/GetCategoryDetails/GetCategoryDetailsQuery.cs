using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQuery : IRequest<CategoryDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
