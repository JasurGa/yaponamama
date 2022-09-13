using System;
using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Providers.Queries.FindProviderPagedList
{
    public class FindProviderPagedListQuery : IRequest<PageDto<ProviderLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

