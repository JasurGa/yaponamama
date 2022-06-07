using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerDetails
{
    public class GetSupplyManagerDetailsQueryHandler : IRequestHandler<GetSupplyManagerDetailsQuery, SupplyManagerDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetSupplyManagerDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<SupplyManagerDetailsVm> Handle(GetSupplyManagerDetailsQuery request, CancellationToken cancellationToken)
        {
            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (supplyManager == null)
            {
                throw new NotFoundException(nameof(SupplyManager), request.Id);
            }

            return _mapper.Map<SupplyManager, SupplyManagerDetailsVm>(supplyManager);
        }
    }
}
