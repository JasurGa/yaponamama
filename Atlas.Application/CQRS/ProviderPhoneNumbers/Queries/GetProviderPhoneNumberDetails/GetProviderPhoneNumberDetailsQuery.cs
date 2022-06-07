using MediatR;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberDetails
{
    public class GetProviderPhoneNumberDetailsQuery : IRequest<ProviderPhoneNumberDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
