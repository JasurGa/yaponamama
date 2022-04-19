using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientList
{
    public class GetCardInfoToClientListQueryHandler : IRequestHandler<GetCardInfoToClientListQuery,
        CardInfoToClientListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCardInfoToClientListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CardInfoToClientListVm> Handle(GetCardInfoToClientListQuery request,
            CancellationToken cancellationToken)
        {
            var cardInfoToClients = await _dbContext.CardInfoToClients
                .Where(e => e.ClientId == request.ClientId)
                .ProjectTo<CardInfoToClientLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new CardInfoToClientListVm { CardInfoToClients = cardInfoToClients };
        }
    }
}
