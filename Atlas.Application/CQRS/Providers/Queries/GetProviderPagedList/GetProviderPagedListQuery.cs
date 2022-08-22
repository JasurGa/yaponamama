using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList
{
    public class GetProviderPagedListQuery : IRequest<PageDto<ProviderLookupDto>>
    {
        public string Search { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
