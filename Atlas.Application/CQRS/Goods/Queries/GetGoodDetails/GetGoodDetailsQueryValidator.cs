using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GetGoodDetailsQueryValidator : AbstractValidator<GetGoodDetailsQuery>
    {
        public GetGoodDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty); 
        }
    }
}
