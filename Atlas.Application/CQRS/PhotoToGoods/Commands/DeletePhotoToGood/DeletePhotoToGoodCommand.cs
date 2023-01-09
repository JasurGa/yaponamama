using System;
using MediatR;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.DeletePhotoToGood
{
	public class DeletePhotoToGoodCommand : IRequest
	{
		public Guid PhotoToGoodId { get; set; }
	}
}

