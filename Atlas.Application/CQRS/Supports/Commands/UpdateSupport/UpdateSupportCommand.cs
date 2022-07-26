using System;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Commands.UpdateSupport
{
    public class UpdateSupportCommand : IRequest
    {
        public Guid Id { get; set; }

        public UpdateUserCommand User { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
