using MediatR;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerDetails
{
    public class GetSupplyManagerDetailsQuery : IRequest<SupplyManagerDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
