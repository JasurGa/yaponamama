using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteAllFavoriteGoods
{
    public class DeleteAllFavoriteGoodsCommandValidator : AbstractValidator<DeleteAllFavoriteGoodsCommand>
    {
        public DeleteAllFavoriteGoodsCommandValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
