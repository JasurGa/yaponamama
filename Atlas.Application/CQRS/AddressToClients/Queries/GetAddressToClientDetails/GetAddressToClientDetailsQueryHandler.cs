using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientDetails
{
    public class GetAddressToClientDetailsQueryHandler :
        IRequestHandler<GetAddressToClientDetailsQuery, AddressToClientDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetAddressToClientDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext)
            => (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<AddressToClientDetailsVm> Handle(GetAddressToClientDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.AddressToClients
                .FirstOrDefaultAsync(e => e.Id == request.Id,
                    cancellationToken);

            if (entity == null || entity.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(AddressToClient), request.Id);
            }

            return _mapper.Map<AddressToClientDetailsVm>(entity);
        }
    }
}
