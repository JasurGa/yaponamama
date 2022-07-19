using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using MediatR;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager
{
    public class UpdateSupplyManagerCommand : IRequest
    {
        public Guid Id { get; set; }

        public UpdateUserCommand User { get; set; }

        public Guid StoreId { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

    }
}
