using System;
using Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Queries.FindSupportsPagedList
{
    public class FindSupportsPagedListQuery : IRequest<PageDto<SupportLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

