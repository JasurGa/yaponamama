using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.DeletePhotoToGood
{
	public class DeletePhotoToGoodCommandValidator : AbstractValidator<DeletePhotoToGoodCommand>
	{
		public DeletePhotoToGoodCommandValidator()
		{
			RuleFor(e => e.PhotoToGoodId)
				.NotEqual(Guid.Empty);
		}
	}
}

