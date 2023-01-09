using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId
{
	public class GetPhotosByGoodIdQueryValidator : AbstractValidator<GetPhotosByGoodIdQuery>
	{
		public GetPhotosByGoodIdQueryValidator()
		{
			RuleFor(e => e.GoodId)
				.NotEqual(Guid.Empty);
		}
	}
}

