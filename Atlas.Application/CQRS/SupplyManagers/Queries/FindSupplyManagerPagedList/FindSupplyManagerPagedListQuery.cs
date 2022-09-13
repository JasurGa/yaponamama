using System;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.FindSupplyManagerPagedList
{
    public class FindSupplyManagerPagedListQuery : IRequest<PageDto<SupplyManagerLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

