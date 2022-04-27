using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class GetPromoDetailsQueryValidator : AbstractValidator<GetPromoDetailsQuery>
    {
        public GetPromoDetailsQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
