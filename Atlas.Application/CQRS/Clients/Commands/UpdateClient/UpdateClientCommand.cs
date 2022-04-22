using System;
using MediatR;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public string PassportPhotoPath { get; set; }
        
        public string SelfieWithPassportPhotoPath { get; set; }
    }
}
