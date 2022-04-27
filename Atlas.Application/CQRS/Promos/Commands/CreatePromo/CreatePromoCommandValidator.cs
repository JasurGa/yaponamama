using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommandValidator : AbstractValidator<CreatePromoCommand>
    {
        public CreatePromoCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.DiscountPrice)
                .NotEmpty();

            RuleFor(p => p.DiscountPercent)
                .NotEmpty();
        }
    }
}
