using Atlas.Application.CQRS.Users.Commands.CreateUser;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.CreateSupport
{
    public class CreateSupportCommand : IRequest<Guid>
    {
        public CreateUserCommand User { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
