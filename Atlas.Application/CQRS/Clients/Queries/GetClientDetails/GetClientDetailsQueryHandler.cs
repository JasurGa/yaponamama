using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientDetails
{
    public class GetClientDetailsQueryHandler : IRequestHandler<GetClientDetailsQuery,
        ClientDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetClientDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ClientDetailsVm> Handle(GetClientDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients
                .Include(x => x.User)
                .Include(x => x.Addresses)
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Id == request.Id, 
                    cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            return _mapper.Map<Client, ClientDetailsVm>(client);
        }
    }
}
