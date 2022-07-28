using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumbers
{
    public class CreateProviderPhoneNumbersCommand : IRequest<List<Guid>>
    {
        public Guid ProviderId { get; set; }

        public List<string> PhoneNumbers { get; set; }
    }
}
