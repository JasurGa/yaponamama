using System;
using MediatR;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreatePhotoToGood
{
	public class CreatePhotoToGoodCommand : IRequest<Guid>
	{
		public Guid GoodId { get; set; }

		public string PhotoPath { get; set; }
	}
}

