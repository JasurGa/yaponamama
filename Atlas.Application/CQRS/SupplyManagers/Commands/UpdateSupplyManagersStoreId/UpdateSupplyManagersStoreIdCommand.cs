using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManagersStoreId
{
    public class UpdateSupplyManagersStoreIdCommand : IRequest
    {
        public Guid StoreId { get; set; }

        public List<Guid> SupplyManagersId { get; set; }
    }
}
