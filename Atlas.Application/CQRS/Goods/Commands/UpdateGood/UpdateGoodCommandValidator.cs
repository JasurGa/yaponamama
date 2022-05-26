using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGood
{
    public class UpdateGoodCommandValidator : AbstractValidator<UpdateGoodCommand>
    {
        public UpdateGoodCommandValidator()
        {
            RuleFor(g => g.Id)
                .NotEqual(Guid.Empty);

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
