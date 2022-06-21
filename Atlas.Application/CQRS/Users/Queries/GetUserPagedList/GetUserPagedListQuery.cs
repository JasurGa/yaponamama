using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Users.Queries.GetUserPagedList
{
    public class GetUserPagedListQuery : IRequest<PageDto<UserLookupDto>>
    {
        public string Search { get; set; }

        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
