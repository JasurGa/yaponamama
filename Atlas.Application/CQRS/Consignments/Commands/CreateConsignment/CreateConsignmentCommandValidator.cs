using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandValidator : AbstractValidator<CreateConsignmentCommand>
    {
        public CreateConsignmentCommandValidator()
        {
            RuleFor(c => c.StoreToGoodId)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.PurchasedAt)
                .NotEmpty();

            RuleFor(c => c.ExpirateAt)
                .NotEmpty();

            RuleFor(c => c.ShelfLocation)
                .NotEmpty();
        }
    }
}
