using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment
{
    public class UpdateConsignmentCommandValidator : AbstractValidator<UpdateConsignmentCommand>
    {
        public UpdateConsignmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

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
