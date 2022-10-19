using System;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.CreateVerificationRequest 
{
    public class CreateVerificationRequestCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string PassportPhotoPath { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }
    }
}