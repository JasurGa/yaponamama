using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandValidator : AbstractValidator<CreateConsignmentCommand>
    {
        public CreateConsignmentCommandValidator()
        {
            RuleFor(x => x.StoreToGoodId)
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
