using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientDetails
{
    public class GetCardInfoToClientDetailsQueryHandler : IRequestHandler<GetCardInfoToClientDetailsQuery,
        CardInfoToClientDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCardInfoToClientDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CardInfoToClientDetailsVm> Handle(GetCardInfoToClientDetailsQuery request, CancellationToken cancellationToken)
        {
            var cardInfoToClient = await _dbContext.CardInfoToClients
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cardInfoToClient == null || cardInfoToClient.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(CardInfoToClient), request.Id);
            }

            return _mapper.Map<CardInfoToClientDetailsVm>(cardInfoToClient);
        }
    }
}
