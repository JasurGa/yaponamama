using FluentValidation;
using System;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGoods
{
    public class DeleteFavoriteGoodsCommandValidator : AbstractValidator<DeleteFavoriteGoodsCommand>
    {
        public DeleteFavoriteGoodsCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
