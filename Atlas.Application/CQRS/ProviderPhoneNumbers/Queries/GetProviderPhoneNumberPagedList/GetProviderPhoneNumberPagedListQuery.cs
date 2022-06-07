using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberPagedList
{
    public class GetProviderPhoneNumberPagedListQuery : IRequest<PageDto<ProviderPhoneNumberLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
