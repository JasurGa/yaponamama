using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.DeletePromoAdvertisePage
{
    public class DeletePromoAdvertisePageCommandValidator : AbstractValidator<DeletePromoAdvertisePageCommand>
    {
        public DeletePromoAdvertisePageCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

