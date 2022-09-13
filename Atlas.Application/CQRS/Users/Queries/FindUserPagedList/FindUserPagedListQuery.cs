using System;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Users.Queries.FindUserPagedList
{
    public class FindUserPagedListQuery : IRequest<PageDto<UserLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

