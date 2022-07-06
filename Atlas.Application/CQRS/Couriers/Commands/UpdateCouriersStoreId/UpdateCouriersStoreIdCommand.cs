using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCouriersStoreId
{
    public class UpdateCouriersStoreIdCommand : IRequest
    {
        public Guid StoreId { get; set; }

        public List<Guid> CourierIds { get; set; }
    }
}
