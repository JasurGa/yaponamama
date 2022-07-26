using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForAdmin
{
    public class GetOrderDetailsForAdminQueryValidator :
        AbstractValidator<GetOrderDetailsForAdminQuery>
    {
        public GetOrderDetailsForAdminQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
