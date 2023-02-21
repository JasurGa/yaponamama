using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommandValidator : AbstractValidator<CreatePromoCommand>
    {
        public CreatePromoCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
