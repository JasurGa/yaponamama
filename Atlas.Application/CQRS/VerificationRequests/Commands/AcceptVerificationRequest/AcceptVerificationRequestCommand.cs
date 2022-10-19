using System;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.AcceptVerificationRequest
{
    public class AcceptVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

