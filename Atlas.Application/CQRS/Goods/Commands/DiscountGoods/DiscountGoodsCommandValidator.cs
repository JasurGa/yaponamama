using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Commands.DiscountGoods
{
    public class DiscountGoodsCommandValidator : AbstractValidator<DiscountGoodsCommand>
    {
        public DiscountGoodsCommandValidator()
        {
            RuleFor(x => x.Discount)
                .NotEmpty();
        }
    }
}
