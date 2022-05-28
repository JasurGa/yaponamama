using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier
{
    public class GetOrderDetailsForCourierQueryValidator : AbstractValidator<GetOrderDetailsForCourierQuery>
    {
        public GetOrderDetailsForCourierQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CourierId)
                .NotEqual(Guid.Empty);
        }
    }
}
