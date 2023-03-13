using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Queries.FindAdminPagedList
{
    public class FindAdminPagedListQueryHandler : IRequestHandler<FindAdminPagedListQuery,
        PageDto<AdminLookupDto>>
    {
        private readonly IMapper         _mapper; 
        private readonly IAtlasDbContext _dbContext;

        public FindAdminPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<AdminLookupDto>> Handle(FindAdminPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var admins = _dbContext.Admins.Include(x => x.User).Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderByDescending(x => EF.Functions.TrigramsSimilarity((x.PhoneNumber + " " + x.User.Login + " " + x.User.FirstName + " " + x.User.LastName + " " + x.User.MiddleName).ToLower().Trim(),
                    request.SearchQuery));

            var adminsCount = await admins.CountAsync(cancellationToken);
            var pagedAdmins = await admins.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<AdminLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = adminsCount,
                PageCount  = (int)Math.Ceiling((double)adminsCount / request.PageSize),
                Data       = _mapper.Map<List<Admin>, List<AdminLookupDto>>(pagedAdmins),
            };
        }
    }
}

