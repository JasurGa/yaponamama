using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.DeletePhotoToGood
{
	public class DeletePhotoToGoodCommandHandler : IRequestHandler<DeletePhotoToGoodCommand>
	{
        private readonly IAtlasDbContext _dbContext;

        public DeletePhotoToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePhotoToGoodCommand request, CancellationToken cancellationToken)
        {
            var photoToGood = await _dbContext.PhotoToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.PhotoToGoodId, cancellationToken);

            if (photoToGood == null)
            {
                throw new NotFoundException(nameof(PhotoToGood), request.PhotoToGoodId);
            }

            _dbContext.PhotoToGoods.Remove(photoToGood);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

