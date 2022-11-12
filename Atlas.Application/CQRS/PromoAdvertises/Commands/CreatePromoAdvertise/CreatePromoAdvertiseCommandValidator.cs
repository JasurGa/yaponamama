using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.CreatePromoAdvertise
{
    public class CreatePromoAdvertiseCommandValidator : AbstractValidator<CreatePromoAdvertiseCommand>
    {
        public CreatePromoAdvertiseCommandValidator()
        {
        }
    }
}

