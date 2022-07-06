using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteAdminCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteAdminCommand request,
            CancellationToken cancellationToken)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (admin == null || admin.IsDeleted)
            {
                throw new NotFoundException(nameof(Admin), request.Id);
            }

            admin.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
