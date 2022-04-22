using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class GetCourierDetailsQueryValidator : AbstractValidator<GetCourierDetailsQuery>
    {
        public GetCourierDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
