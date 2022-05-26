using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Commands.CreateGood
{
    public class CreateGoodCommandValidator : AbstractValidator<CreateGoodCommand>
    {
        public CreateGoodCommandValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty();

            RuleFor(g => g.Description)
                .NotEmpty();

            RuleFor(g => g.PhotoPath)
                .NotEmpty();

            RuleFor(g => g.SellingPrice)
                .GreaterThan(x => x.PurchasePrice);

            RuleFor(g => g.PurchasePrice)
                .NotEmpty();

            RuleFor(g => g.ProviderId)
                .NotEqual(Guid.Empty);

        }
    }
}
