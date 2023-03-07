using MediatR;
using System;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Login { get; set; }
    }
}
