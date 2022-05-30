using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberList;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId
{
    public class GetProviderPhoneNumberListByProviderIdQueryHandler : IRequestHandler<GetProviderPhoneNumberListByProviderIdQuery, ProviderPhoneNumberListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderPhoneNumberListByProviderIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProviderPhoneNumberListVm> Handle(GetProviderPhoneNumberListByProviderIdQuery request, CancellationToken cancellationToken)
        {
            var providerPhoneNumbers = await _dbContext.ProviderPhoneNumbers
                .Where(x => x.ProviderId == request.ProviderId)
                .ProjectTo<ProviderPhoneNumberLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProviderPhoneNumberListVm { ProviderPhoneNumbers = providerPhoneNumbers };
        }
    }
}
