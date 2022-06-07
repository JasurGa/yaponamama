using System;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Commands.RestoreSupport
{
    public class RestoreSupportCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
