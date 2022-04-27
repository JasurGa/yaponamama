using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Commands.UpdatePromo
{
    public class UpdatePromoCommandValidator : AbstractValidator<UpdatePromoCommand>
    {
        public UpdatePromoCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);

            RuleFor(p => p.Name)
               .NotEmpty();

            RuleFor(p => p.DiscountPrice)
               .NotEmpty();

            RuleFor(p => p.DiscountPercent)
               .NotEmpty();
        }
    }
}
