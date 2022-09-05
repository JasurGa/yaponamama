using System;
using MediatR;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public UpdateClientCommand User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
        
        public string SelfieWithPassportPhotoPath { get; set; }

        public bool IsPassportVerified { get; set; }
    }
}
