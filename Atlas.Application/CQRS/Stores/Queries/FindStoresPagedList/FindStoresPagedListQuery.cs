using System;
using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Queries.FindStoresPagedList
{
    public class FindStoresPagedListQuery : IRequest<PageDto<StoreLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

