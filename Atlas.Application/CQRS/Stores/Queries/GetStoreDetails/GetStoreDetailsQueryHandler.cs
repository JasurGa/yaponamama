using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreDetails
{
    public class GetStoreDetailsQueryHandler : IRequestHandler<GetStoreDetailsQuery, StoreDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;
        public GetStoreDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<StoreDetailsVm> Handle(GetStoreDetailsQuery request, CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.Id);
            }

            return _mapper.Map<StoreDetailsVm>(store);
        }
    }
}
