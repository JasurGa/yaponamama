using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood
{
    public class CreatePromoToGoodCommandValidator : AbstractValidator<CreatePromoToGoodCommand>
    {
        public CreatePromoToGoodCommandValidator()
        {
            RuleFor(c => c.PromoId)
                .NotEqual(Guid.Empty);
        }
    }
}
