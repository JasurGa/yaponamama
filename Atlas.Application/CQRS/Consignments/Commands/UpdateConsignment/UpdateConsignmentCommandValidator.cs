using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment
{
    public class UpdateConsignmentCommandValidator : AbstractValidator<UpdateConsignmentCommand>
    {
        public UpdateConsignmentCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);

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
