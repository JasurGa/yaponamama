using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Clients.Queries.FindClientPagedList
{
    public class FindClientPagedListQueryHandler : IRequestHandler<FindClientPagedListQuery,
        PageDto<ClientLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindClientPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ClientLookupDto>> Handle(FindClientPagedListQuery request, CancellationToken cancellationToken)
        {
            var clients = _dbContext.Clients.OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.Id}",
                request.SearchQuery));

            var clientsCount = await clients.CountAsync(cancellationToken);
            var pagedClients = await clients.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<ClientLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = clientsCount,
                PageCount  = (int)Math.Ceiling((double)clientsCount / request.PageSize),
                Data       = _mapper.Map<List<Client>, List<ClientLookupDto>>(pagedClients),
            };
        }
    }
}

