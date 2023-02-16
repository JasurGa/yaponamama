using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.DeletePromoToGood
{
    public class DeletePromoToGoodCommandValidator : 
        AbstractValidator<DeletePromoToGoodCommand>
    {
        public DeletePromoToGoodCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
