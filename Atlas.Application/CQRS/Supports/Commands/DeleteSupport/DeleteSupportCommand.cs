using System;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Commands.DeleteSupport
{
    public class DeleteSupportCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
