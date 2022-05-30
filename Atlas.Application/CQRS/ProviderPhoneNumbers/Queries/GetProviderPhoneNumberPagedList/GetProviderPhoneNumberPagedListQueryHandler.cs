using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberPagedList
{
    public class GetProviderPhoneNumberPagedListQueryHandler : IRequestHandler<GetProviderPhoneNumberPagedListQuery, PageDto<ProviderPhoneNumberLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderPhoneNumberPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ProviderPhoneNumberLookupDto>> Handle(GetProviderPhoneNumberPagedListQuery request, CancellationToken cancellationToken)
        {
            var providerPhoneNumbersCount = await _dbContext.ProviderPhoneNumbers.CountAsync(cancellationToken);

            var providerPhoneNumbers = await _dbContext.ProviderPhoneNumbers
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProviderPhoneNumberLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ProviderPhoneNumberLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = providerPhoneNumbersCount,
                PageCount  = (int)Math.Ceiling((double)providerPhoneNumbersCount / request.PageSize),
                Data       = providerPhoneNumbers
            };
        }
    }
}
