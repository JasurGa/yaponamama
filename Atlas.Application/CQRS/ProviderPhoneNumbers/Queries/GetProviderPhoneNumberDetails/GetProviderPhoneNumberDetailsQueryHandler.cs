using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberDetails
{
    public class GetProviderPhoneNumberDetailsQueryHandler : IRequestHandler<GetProviderPhoneNumberDetailsQuery, ProviderPhoneNumberDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderPhoneNumberDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProviderPhoneNumberDetailsVm> Handle(GetProviderPhoneNumberDetailsQuery request, CancellationToken cancellationToken)
        {
            var providerPhoneNumber = await _dbContext.ProviderPhoneNumbers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (providerPhoneNumber == null)
            {
                throw new NotFoundException(nameof(ProviderPhoneNumber), request.Id);
            }

            return _mapper.Map<ProviderPhoneNumber, ProviderPhoneNumberDetailsVm>(providerPhoneNumber);
        }
    }
}
