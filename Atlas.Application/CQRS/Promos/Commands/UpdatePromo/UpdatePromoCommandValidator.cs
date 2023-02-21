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
        }
    }
}
