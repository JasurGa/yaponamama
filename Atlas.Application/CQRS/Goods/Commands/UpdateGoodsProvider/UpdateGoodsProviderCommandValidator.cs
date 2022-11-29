using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGoodsProvider
{
    public class UpdateGoodsProviderCommandValidator : AbstractValidator<UpdateGoodsProviderCommand>
    {
        public UpdateGoodsProviderCommandValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodIds)
                .NotEmpty();
        }
    }
}