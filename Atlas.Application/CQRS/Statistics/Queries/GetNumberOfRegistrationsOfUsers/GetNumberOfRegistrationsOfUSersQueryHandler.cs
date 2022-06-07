using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Statistics.Queries.GetNumberOfRegistrationsOfUsers
{
    public class GetNumberOfRegistrationsOfUSersQueryHandler : IRequestHandler<GetNumberOfRegistrationsOfUsersQuery, long>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetNumberOfRegistrationsOfUSersQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<long> Handle(GetNumberOfRegistrationsOfUsersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                    .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt < request.EndDate)
                    .CountAsync(cancellationToken);
        }
    }
}
