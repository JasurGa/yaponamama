using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId
{
    public class GetPromoToGoodsByPromoIdQueryValidator : 
        AbstractValidator<GetPromoToGoodsByPromoIdQuery>
    {
        public GetPromoToGoodsByPromoIdQueryValidator()
        {
            RuleFor(c => c.PromoId)
                .NotEqual(Guid.Empty);
        }
    }
}
