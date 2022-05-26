using MediatR;
using System;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class GetCourierDetailsQuery : IRequest<CourierDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
