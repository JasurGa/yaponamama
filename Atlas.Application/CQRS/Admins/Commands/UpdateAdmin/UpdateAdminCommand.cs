using System;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Commands.UpdateAdmin
{
    public class UpdateAdminCommand : IRequest
    {
        public Guid Id { get; set; }

        public long KPI { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public Guid OfficialRoleId { get; set; }

        public UpdateUserCommand User { get; set; }
    }
}
