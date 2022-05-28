using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
    {
        public GetOrderDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
