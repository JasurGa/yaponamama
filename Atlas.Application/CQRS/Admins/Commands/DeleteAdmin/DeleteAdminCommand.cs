using System;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
