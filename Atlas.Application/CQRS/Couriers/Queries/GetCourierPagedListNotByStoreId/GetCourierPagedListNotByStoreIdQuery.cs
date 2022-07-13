using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedListNotByStoreId
{
    public class GetCourierPagedListNotByStoreIdQuery : IRequest<PageDto<CourierLookupDto>>
    {
        public Guid StoreId { get; set; }

        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
