using MediatR;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId
{
    public class GetProviderPhoneNumberListByProviderIdQuery : IRequest<ProviderPhoneNumberListVm>
    {
        public Guid ProviderId { get; set; }
    }
}
