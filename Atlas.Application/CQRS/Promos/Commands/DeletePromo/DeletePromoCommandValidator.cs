using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.DeletePromo
{
    public class DeletePromoCommandValidator : AbstractValidator<DeletePromoCommand>
    {
        public DeletePromoCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
