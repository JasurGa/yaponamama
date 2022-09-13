using System;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Queries.FindCourierPagedList
{
    public class FindCourierPagedListQuery : IRequest<PageDto<CourierLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public Guid? FilterStoreId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

