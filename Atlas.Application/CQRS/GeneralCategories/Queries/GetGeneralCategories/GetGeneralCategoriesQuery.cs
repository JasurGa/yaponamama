using System;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories
{
    public class GetGeneralCategoriesQuery : IRequest<GeneralCategoriesListVm>
    {
        public bool ShowDeleted { get; set; }
    }
}
