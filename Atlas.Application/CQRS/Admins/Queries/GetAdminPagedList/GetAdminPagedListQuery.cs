using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList
{
    public class GetAdminPagedListQuery : IRequest<PageDto<AdminLookupDto>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool ShowDeleted { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; } 
    }
}
