using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderDetails
{
    public class GetProviderDetailsQueryHandler : IRequestHandler<GetProviderDetailsQuery, ProviderDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProviderDetailsVm> Handle(GetProviderDetailsQuery request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.Id);
            }

            return _mapper.Map<Provider, ProviderDetailsVm>(provider);
        }
    }
}
