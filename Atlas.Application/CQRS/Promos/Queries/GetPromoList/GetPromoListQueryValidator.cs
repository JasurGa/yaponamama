using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class GetPromoListQueryValidator : AbstractValidator<GetPromoListQuery>
    {
        public GetPromoListQueryValidator()
        {

        }
    }
}
