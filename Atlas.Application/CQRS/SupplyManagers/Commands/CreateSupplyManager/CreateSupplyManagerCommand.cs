using System;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager
{
    public class CreateSupplyManagerCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
