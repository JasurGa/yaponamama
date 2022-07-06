using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminDetails
{
    public class GetAdminDetailsQueryHandler : IRequestHandler<GetAdminDetailsQuery,
        AdminDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetAdminDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<AdminDetailsVm> Handle(GetAdminDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var admin = await _dbContext.Admins.Include(x => x.User)
                .Include(x => x.OfficialRole)
                .FirstOrDefaultAsync(x =>
                    x.Id == request.Id, cancellationToken);

            if (admin == null)
            {
                throw new NotFoundException(nameof(Admin), request.Id);
            }

            return _mapper.Map<Admin, AdminDetailsVm>(admin);
        }
    }
}
