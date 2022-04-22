using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class GetLastOrdersPagedListByCourierQueryValidator : AbstractValidator<GetLastOrdersPagedListByCourierQuery>
    {
        public GetLastOrdersPagedListByCourierQueryValidator()
        {
            RuleFor(o => o.CourierId)
                .NotEqual(Guid.Empty);

            RuleFor(o => o.PageSize)
                .NotEmpty();
        }
    }
}
