using Atlas.Application.CQRS.FavoriteGoods.Commands.CreateManyFavoriteGoods;
using FluentValidation;
using System;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGoods
{
    public class CreateFavoriteGoodsCommandValidator : AbstractValidator<CreateFavoriteGoodsCommand>
    {
        public CreateFavoriteGoodsCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
