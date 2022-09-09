using System;
using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Clients.Queries.FindClientPagedList
{
    public class FindClientPagedListQuery : IRequest<PageDto<ClientLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

