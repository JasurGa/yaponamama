using MediatR;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.UpdateProviderPhoneNumber
{
    public class UpdateProviderPhoneNumberCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
