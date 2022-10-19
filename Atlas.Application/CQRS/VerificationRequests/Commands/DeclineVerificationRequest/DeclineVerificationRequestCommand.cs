using System;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.DeclineVerificationRequest
{
    public class DeclineVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }
    }
}

