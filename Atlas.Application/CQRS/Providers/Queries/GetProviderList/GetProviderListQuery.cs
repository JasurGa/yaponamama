using MediatR;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class GetProviderListQuery : IRequest<ProviderListVm>
    {
        public string Search { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
