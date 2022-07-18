using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportDetails
{
    public class GetSupportDetailsQueryHandler : IRequestHandler<GetSupportDetailsQuery, SupportDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetSupportDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<SupportDetailsVm> Handle(GetSupportDetailsQuery request, CancellationToken cancellationToken)
        {
            var support = await _dbContext.Supports.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (support == null)
            {
                throw new NotFoundException(nameof(Support), request.Id);
            }

            return _mapper.Map<Support, SupportDetailsVm>(support);
        }
    }
}
