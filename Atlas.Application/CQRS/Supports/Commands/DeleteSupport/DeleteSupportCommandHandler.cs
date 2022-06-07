using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Supports.Commands.DeleteSupport
{
    public class DeleteSupportCommandHandler : IRequestHandler<DeleteSupportCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteSupportCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteSupportCommand request, CancellationToken cancellationToken)
        {
            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (support == null || support.IsDeleted)
            {
                throw new NotFoundException(nameof(Support), request.Id);
            }

            support.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
