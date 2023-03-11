using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class GetLastOrdersPagedListByCourierQueryValidator : AbstractValidator<GetLastOrdersPagedListByCourierQuery>
    {
        public GetLastOrdersPagedListByCourierQueryValidator()
        {
            RuleFor(x => x.CourierId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

        }
    }
}
