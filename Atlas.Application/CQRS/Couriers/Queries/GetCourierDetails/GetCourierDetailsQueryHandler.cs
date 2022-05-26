using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class GetCourierDetailsQueryHandler : IRequestHandler<GetCourierDetailsQuery, CourierDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCourierDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) => 
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CourierDetailsVm> Handle(GetCourierDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(c => 
                c.Id == request.Id, cancellationToken);

            if (courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }

            return _mapper.Map<Courier, CourierDetailsVm>(courier);
        }
    }
}
