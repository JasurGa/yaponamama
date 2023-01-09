using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreatePhotoToGood
{
	public class CreatePhotoToGoodCommandHandler : IRequestHandler<CreatePhotoToGoodCommand,
		Guid>
	{
		private readonly IAtlasDbContext _dbContext;

		public CreatePhotoToGoodCommandHandler(IAtlasDbContext dbContext) =>
			_dbContext = dbContext;

        public async Task<Guid> Handle(CreatePhotoToGoodCommand request, CancellationToken cancellationToken)
        {
			var good = await _dbContext.Goods.FirstOrDefaultAsync(x => x.Id == request.GoodId,
				cancellationToken);

			if (good == null)
			{
				throw new NotFoundException(nameof(Good), request.GoodId);
			}

			var photoToGood = new PhotoToGood
			{
				Id = Guid.NewGuid(),
				GoodId = request.GoodId,
				PhotoPath = request.PhotoPath
			};

			await _dbContext.PhotoToGoods.AddAsync(photoToGood,
				cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return photoToGood.Id;
        }
    }
}

