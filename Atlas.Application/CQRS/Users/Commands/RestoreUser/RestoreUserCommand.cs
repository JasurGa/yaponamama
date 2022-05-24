using System;
using MediatR;

namespace Atlas.Application.CQRS.Users.Commands.RestoreUser
{
    public class RestoreUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
