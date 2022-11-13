using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryDetails
{
    public class GetPromoCategoryDetailsQuery : IRequest<PromoCategoryDetailsVm>
    {
        public Guid Id { get; set; }
    }
}

