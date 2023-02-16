using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.UpdatePromo
{
    public class UpdatePromoCommandValidator : AbstractValidator<UpdatePromoCommand>
    {
        public UpdatePromoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
               .NotEmpty();

            RuleFor(x => x.DiscountPrice)
               .NotEmpty();

            RuleFor(x => x.DiscountPercent)
               .NotEmpty();
        }
    }
}
