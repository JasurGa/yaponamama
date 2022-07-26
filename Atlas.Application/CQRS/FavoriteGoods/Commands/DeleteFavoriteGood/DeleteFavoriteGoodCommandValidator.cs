using System;
using FluentValidation;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGood
{
    public class DeleteFavoriteGoodCommandValidator : AbstractValidator<DeleteFavoriteGoodCommand>
    {
        public DeleteFavoriteGoodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
