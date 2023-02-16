using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.UpdatePromoToGood
{
    public class UpdatePromoToGoodCommandValidator : 
        AbstractValidator<UpdatePromoToGoodCommand>
    {
        public UpdatePromoToGoodCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.PromoId)
                .NotEqual(Guid.Empty);
            
            RuleFor(c => c.GoodId)
                .NotEqual(Guid.Empty);
        }
    }
}
