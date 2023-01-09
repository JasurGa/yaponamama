using System;
using MediatR;

namespace Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId
{
	public class GetPhotosByGoodIdQuery : IRequest<PhotoToGoodListVm>
	{
		public Guid GoodId { get; set; }
	}
}

