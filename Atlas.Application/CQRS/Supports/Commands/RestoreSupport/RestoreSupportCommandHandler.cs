using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Supports.Commands.RestoreSupport
{
    public class RestoreSupportCommandHandler : IRequestHandler<RestoreSupportCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreSupportCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreSupportCommand request, CancellationToken cancellationToken)
        {
            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (support == null || !support.IsDeleted)
            {
                throw new NotFoundException(nameof(Support), request.Id);
            }

            support.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
