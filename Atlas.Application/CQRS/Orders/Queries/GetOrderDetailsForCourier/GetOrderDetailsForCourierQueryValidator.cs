using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier
{
    public class GetOrderDetailsForCourierQueryValidator : AbstractValidator<GetOrderDetailsForCourierQuery>
    {
        public GetOrderDetailsForCourierQueryValidator()
        {
            RuleFor(o => o.Id)
                .NotEqual(Guid.Empty);

            RuleFor(o => o.CourierId)
                .NotEqual(Guid.Empty);
        }
    }
}
