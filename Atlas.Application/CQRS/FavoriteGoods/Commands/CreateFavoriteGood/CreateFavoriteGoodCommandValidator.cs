using System;
using FluentValidation;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGood
{
    public class CreateFavoriteGoodCommandValidator : AbstractValidator<CreateFavoriteGoodCommand>
    {
        public CreateFavoriteGoodCommandValidator()
        {
            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
