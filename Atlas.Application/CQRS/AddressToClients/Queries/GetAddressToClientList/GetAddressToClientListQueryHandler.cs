using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList
{
    public class GetAddressToClientListQueryHandler :
        IRequestHandler<GetAddressToClientListQuery,
            AddressToClientListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetAddressToClientListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<AddressToClientListVm> Handle(GetAddressToClientListQuery request,
            CancellationToken cancellationToken)
        {
            var addressToClientsQuery = await _dbContext.AddressToClients
                .Where(e => e.ClientId == request.ClientId)
                .ProjectTo<AddressToClientLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AddressToClientListVm { AddressToClients = addressToClientsQuery };
        }
    }
}
