using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Users.Queries.FindUserPagedList
{
    public class FindUserPagedListQueryHandler : IRequestHandler<FindUserPagedListQuery,
        PageDto<UserLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindUserPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<UserLookupDto>> Handle(FindUserPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var users = _dbContext.Users.Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance(
                    $"{x.Login} {x.FirstName} {x.LastName} {x.MiddleName}".ToLower().Trim(),
                        request.SearchQuery));

            var usersCount = await users.CountAsync(cancellationToken);
            var pagedUsers = await users.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<UserLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = usersCount,
                PageCount  = (int)Math.Ceiling((double)usersCount / request.PageSize),
                Data       = _mapper.Map<List<User>, List<UserLookupDto>>(pagedUsers),
            };
        }
    }
}

