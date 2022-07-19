using System;
using Atlas.Application.CQRS.Users.Commands.CreateUser;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager
{
    public class CreateSupplyManagerCommand : IRequest<Guid>
    {
        public CreateUserCommand User { get; set; }

        public Guid StoreId { get; set; }

        public long WorkingDayDuration { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public int Salary { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
