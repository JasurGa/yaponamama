using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList
{
    public class GetCourierPagedListQuery : IRequest<PageDto<CourierLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
