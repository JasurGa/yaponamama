using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentDetails
{
    public class GetDisposeToConsignmentDetailsQueryHandler : IRequestHandler<GetDisposeToConsignmentDetailsQuery, DisposeToConsignmentDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetDisposeToConsignmentDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<DisposeToConsignmentDetailsVm> Handle(GetDisposeToConsignmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var disposeToConsignment = await _dbContext.DisposeToConsignments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (disposeToConsignment == null)
            {
                throw new NotFoundException(nameof(disposeToConsignment), request.Id);
            }

            return _mapper.Map<DisposeToConsignment, DisposeToConsignmentDetailsVm>(disposeToConsignment);
        }
    }
}
