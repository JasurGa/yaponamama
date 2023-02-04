using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandValidator : AbstractValidator<CreateConsignmentCommand>
    {
        public CreateConsignmentCommandValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThan(0);

            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PurchasedAt)
                .NotEmpty();

            RuleFor(x => x.ExpirateAt)
                .GreaterThan(x => x.PurchasedAt);

            RuleFor(x => x.ShelfLocation)
                .NotEmpty();
        }
    }
}
