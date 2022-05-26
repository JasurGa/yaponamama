using MediatR;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class GetStoreListQuery : IRequest<StoreListVm>
    {
        public bool ShowDeleted { get; set; }
    }
}
