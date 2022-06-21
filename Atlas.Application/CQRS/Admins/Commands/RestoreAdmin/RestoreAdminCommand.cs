using System;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Commands.RestoreAdmin
{
    public class RestoreAdminCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
