using System;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<Guid>
    {
        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public Guid OfficialRoleId { get; set; }

        public Guid UserId { get; set; }
    }
}
