using MediatR;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumber
{
    public class CreateProviderPhoneNumberCommand : IRequest<Guid>
    {
        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
