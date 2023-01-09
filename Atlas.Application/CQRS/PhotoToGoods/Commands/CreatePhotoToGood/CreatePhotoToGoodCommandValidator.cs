using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreatePhotoToGood
{
	public class CreatePhotoToGoodCommandValidator : AbstractValidator<CreatePhotoToGoodCommand>
	{
		public CreatePhotoToGoodCommandValidator()
		{
			RuleFor(e => e.GoodId)
				.NotEqual(Guid.Empty);

			RuleFor(e => e.PhotoPath)
				.NotEmpty();
		}
	}
}

