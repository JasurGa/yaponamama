using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionDetails
{
    public class GetCorrectionDetailsQueryHandler : IRequestHandler<GetCorrectionDetailsQuery,
        CorrectionDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCorrectionDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CorrectionDetailsVm> Handle(GetCorrectionDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var correction = await _dbContext.Corrections
                .Include(x => x.StoreToGood)
                .Include(x => x.StoreToGood.Good)
                .Include(x => x.StoreToGood.Store)
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);

            if (correction == null)
            {
                throw new NotFoundException(nameof(Correction), request.Id);
            }

            return _mapper.Map<Correction, CorrectionDetailsVm>(correction);
        }
    }
}
