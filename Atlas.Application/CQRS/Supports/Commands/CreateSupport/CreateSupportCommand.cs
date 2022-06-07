using MediatR;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.CreateSupport
{
    public class CreateSupportCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
