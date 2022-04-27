using MediatR;
using System.Threading.Tasks;
using AutoMapper;
using Atlas.Application.Interfaces;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;
using Atlas.Domain;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class GetPromoDetailsQueryHandler : IRequestHandler<GetPromoDetailsQuery, PromoDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) => 
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoDetailsVm> Handle(GetPromoDetailsQuery request, CancellationToken cancellationToken)
        {
            var promo = await _dbContext.Promos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(promo == null)
            {
                throw new NotFoundException(nameof(Promo), request.Id);
            }

            return _mapper.Map<PromoDetailsVm>(promo);
        }
    }
}
