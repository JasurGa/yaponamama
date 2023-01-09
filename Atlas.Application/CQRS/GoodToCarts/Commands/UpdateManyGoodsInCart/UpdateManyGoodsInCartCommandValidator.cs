using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart
{
	public class UpdateManyGoodsInCartCommandValidator : AbstractValidator<UpdateManyGoodsInCartCommand>
	{
		public UpdateManyGoodsInCartCommandValidator()
		{
			RuleFor(e => e.ClientId)
				.NotEqual(Guid.Empty);
		}
	}
}

