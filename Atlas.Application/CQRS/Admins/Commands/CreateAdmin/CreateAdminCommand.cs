﻿using System;
using Atlas.Application.CQRS.Users.Commands.CreateUser;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<Guid>
    {
        public string PhoneNumber { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public Guid OfficialRoleId { get; set; }

        public CreateUserCommand User { get; set; }
    }
}
