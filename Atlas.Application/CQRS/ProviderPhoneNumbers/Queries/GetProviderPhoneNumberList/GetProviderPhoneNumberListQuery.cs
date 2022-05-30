using MediatR;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberList
{
    public class GetProviderPhoneNumberListQuery : IRequest<ProviderPhoneNumberListVm>
    {
    }
}
