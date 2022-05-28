using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Commands.CreateGood
{
    public class CreateGoodCommandValidator : AbstractValidator<CreateGoodCommand>
    {
        public CreateGoodCommandValidator()
        {
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

        }
    }
}
