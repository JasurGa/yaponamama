using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList
{
    public class GetProviderPagedListQuery : IRequest<PageDto<ProviderLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
