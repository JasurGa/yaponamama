using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.OfficialRoles.Queries.GetOfficialRolesListQuery
{
    public class GetOfficialRolesListQueryHandler : IRequestHandler
        <GetOfficialRolesListQuery, OfficialRolesListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOfficialRolesListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OfficialRolesListVm> Handle(GetOfficialRolesListQuery request,
            CancellationToken cancellationToken)
        {
            var officialRoles = await _dbContext.OfficialRoles
                .ProjectTo<OfficialRoleLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new OfficialRolesListVm
            {
                OfficialRoles = officialRoles
            };
        }
    }
}
