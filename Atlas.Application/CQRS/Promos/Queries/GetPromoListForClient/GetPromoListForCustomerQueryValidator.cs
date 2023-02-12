using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListForClient
{
    public class GetPromoListForCustomerQueryValidator : 
        AbstractValidator<GetPromoListForClientQuery>
    {
        public GetPromoListForCustomerQueryValidator() 
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
