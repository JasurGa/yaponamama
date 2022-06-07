using MediatR;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager
{
    public class UpdateSupplyManagerCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
