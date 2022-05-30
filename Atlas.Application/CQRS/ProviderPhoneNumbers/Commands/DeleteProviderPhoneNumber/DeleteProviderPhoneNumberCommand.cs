using MediatR;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.DeleteProviderPhoneNumber
{
    public class DeleteProviderPhoneNumberCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
