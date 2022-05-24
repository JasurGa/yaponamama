using System;
using MediatR;

namespace Atlas.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
