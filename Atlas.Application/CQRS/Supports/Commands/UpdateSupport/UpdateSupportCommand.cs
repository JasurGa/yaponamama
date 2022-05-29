using System;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Commands.UpdateSupport
{
    public class UpdateSupportCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }
    }
}
