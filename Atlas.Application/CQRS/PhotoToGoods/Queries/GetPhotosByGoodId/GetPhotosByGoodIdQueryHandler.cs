using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId
{
	public class GetPhotosByGoodIdQueryHandler : IRequestHandler<GetPhotosByGoodIdQuery,
		PhotoToGoodListVm>
	{
		private readonly IMapper _mapper;

		private readonly IAtlasDbContext _dbContext;

		public GetPhotosByGoodIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
			(_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PhotoToGoodListVm> Handle(GetPhotosByGoodIdQuery request, CancellationToken cancellationToken)
        {
			var photosToGoods = await _dbContext.PhotoToGoods
				.Where(x => x.GoodId == request.GoodId)
				.ProjectTo<PhotoToGoodLookupDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new PhotoToGoodListVm
			{
				PhotosToGoods = photosToGoods
			};
        }
    }
}

