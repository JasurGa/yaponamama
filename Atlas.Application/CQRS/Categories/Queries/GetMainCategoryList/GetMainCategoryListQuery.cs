using MediatR;
using System;

namespace Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList
{
    public class GetMainCategoryListQuery : IRequest<MainCategoryListVm>
    {
        public bool ShowDeleted { get; set; }

        public bool ShowHidden { get; set; }

        public Guid ClientId { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
