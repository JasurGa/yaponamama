using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberList
{
    public class GetProviderPhoneNumberListQueryHandler : IRequestHandler<GetProviderPhoneNumberListQuery, ProviderPhoneNumberListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderPhoneNumberListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProviderPhoneNumberListVm> Handle(GetProviderPhoneNumberListQuery request, CancellationToken cancellationToken)
        {
            var providerPhoneNumbers = await _dbContext.ProviderPhoneNumbers
                .ProjectTo<ProviderPhoneNumberLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProviderPhoneNumberListVm { ProviderPhoneNumbers = providerPhoneNumbers };
        }
    }
}
