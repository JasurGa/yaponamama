using System;
using Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategoryById
{
    public class GetGeneralCategoryByIdQuery : IRequest<GeneralCategoryLookupDto>
    {
        public Guid Id { get; set; }
    }
}
