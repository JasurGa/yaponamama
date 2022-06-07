using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Users.Queries.GetUserPagedList
{
    public class GetUserPagedListQueryHandler : IRequestHandler<GetUserPagedListQuery, PageDto<UserLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetUserPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<UserLookupDto>> Handle(GetUserPagedListQuery request, CancellationToken cancellationToken)
        {
            var usersCount = await _dbContext.Users
                .Where(x =>
                    x.FirstName.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()) ||
                    x.LastName.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()) ||
                    x.Login.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()))
                .CountAsync(x => x.IsDeleted == request.ShowDeleted,
                    cancellationToken);

            var users = await _dbContext.Users
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .Where(x =>
                    x.FirstName.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()) || 
                    x.LastName.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()) ||
                    x.Login.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()))
                .ToListAsync(cancellationToken);

            return new PageDto<UserLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = usersCount,
                PageCount  = (int)Math.Ceiling((double)usersCount / request.PageSize),
                Data       = users
            };
        }
    }
}
