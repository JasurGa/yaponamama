using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Corrections.Commands.UpdateCorrectionCausedBy
{
    public class UpdateCorrectionCausedByCommandHandler : IRequestHandler<UpdateCorrectionCausedByCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateCorrectionCausedByCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCorrectionCausedByCommand request, CancellationToken cancellationToken)
        {
            var correction = await _dbContext.Corrections.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (correction == null)
            {
                throw new NotFoundException(nameof(Correction), request.Id);
            }

            correction.CauseBy = request.CausedBy;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
