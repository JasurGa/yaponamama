using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetPromoAdvertiseById
{
    public class GetPromoAdvertiseByIdQueryValidator : AbstractValidator<GetPromoAdvertiseByIdQuery>
    {
        public GetPromoAdvertiseByIdQueryValidator() 
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
