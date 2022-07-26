using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin
{
    public class GetLastOrdersPagedListByAdminQueryValidator :
        AbstractValidator<GetLastOrdersPagedListByAdminQuery>
    {
        public GetLastOrdersPagedListByAdminQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
