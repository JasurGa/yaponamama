using System;
using Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Queries.FindAdminPagedList
{
    public class FindAdminPagedListQuery : IRequest<PageDto<AdminLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

