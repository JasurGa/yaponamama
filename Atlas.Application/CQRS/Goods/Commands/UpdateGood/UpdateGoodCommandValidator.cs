using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGood
{
    public class UpdateGoodCommandValidator : AbstractValidator<UpdateGoodCommand>
    {
        public UpdateGoodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.PhotoPath)
                .NotEmpty();

            RuleFor(x => x.SellingPrice)
                .GreaterThan(x => x.PurchasePrice);

            RuleFor(x => x.PurchasePrice)
                .NotEmpty();

            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Mass)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Volume)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
