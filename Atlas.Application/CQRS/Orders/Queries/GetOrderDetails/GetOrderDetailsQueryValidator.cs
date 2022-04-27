using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
    {
        public GetOrderDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
