using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PageVisits.Commands.IncrementPageVisit
{
    public class IncrementPageVisitCommandHandler :
        IRequestHandler<IncrementPageVisitCommand, int>
    {
        private readonly IAtlasDbContext _dbContext;

        public IncrementPageVisitCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<int> Handle(IncrementPageVisitCommand request,
            CancellationToken cancellationToken)
        {
            var page = await _dbContext.PageVisits.FirstOrDefaultAsync(x =>
                x.Path == request.Path, cancellationToken);

            if (page == null)
            {
                page = new PageVisit
                {
                    Id           = Guid.NewGuid(),
                    Path         = request.Path,
                    VisitedCount = 1
                };

                await _dbContext.PageVisits.AddAsync(page, cancellationToken);
            }
            else
            {
                page.VisitedCount += 1;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return page.VisitedCount;
        }
    }
}
