using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientPagedList
{
    public class GetClientPagedListQueryHandler : IRequestHandler<GetClientPagedListQuery, PageDto<ClientLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetClientPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ClientLookupDto>> Handle(GetClientPagedListQuery request, CancellationToken cancellationToken)
        {
            var clientsQuery = _dbContext.Clients.Include(x => x.User)
                .Where(x => x.IsDeleted == request.ShowDeleted);

            if (request.FilterFromCreatedAt != null)
            {
                clientsQuery = clientsQuery.Where(x => x.User.CreatedAt >= request.FilterFromCreatedAt);
            }
            if (request.FilterToCreatedAt != null)
            {
                clientsQuery = clientsQuery.Where(x => x.User.CreatedAt <= request.FilterToCreatedAt);
            }

            var clientsCount = await clientsQuery.CountAsync(cancellationToken);

            var clients = await clientsQuery
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Include(x => x.User)
                .ProjectTo<ClientLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ClientLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = clientsCount,
                PageCount  = (int)Math.Ceiling((double)clientsCount / request.PageSize),
                Data       = clients,
            };
        }
    }
}
