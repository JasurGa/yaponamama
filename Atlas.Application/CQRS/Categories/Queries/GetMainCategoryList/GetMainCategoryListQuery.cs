using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList
{
    public class GetMainCategoryListQuery : IRequest<MainCategoryListVm>
    {
        public bool ShowDeleted { get; set; }
    }
}
