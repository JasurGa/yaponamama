using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.DeletePromo
{
    public class DeletePromoCommandValidator : AbstractValidator<DeletePromoCommand>
    {
        public DeletePromoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
