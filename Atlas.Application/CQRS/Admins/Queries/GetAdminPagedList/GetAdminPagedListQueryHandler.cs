using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Extensions;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList
{
    public class GetAdminPagedListQueryHandler : IRequestHandler<GetAdminPagedListQuery,
        PageDto<AdminLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetAdminPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<AdminLookupDto>> Handle(GetAdminPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var adminsCount = await _dbContext.Admins.CountAsync(x => 
                x.IsDeleted == request.ShowDeleted, cancellationToken);

            var admins = await _dbContext.Admins
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Include(x => x.User)
                .ProjectTo<AdminLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<AdminLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = adminsCount,
                PageCount  = (int)Math.Ceiling((double)adminsCount / request.PageSize),
                Data       = admins,
            };
        }
    }
}
