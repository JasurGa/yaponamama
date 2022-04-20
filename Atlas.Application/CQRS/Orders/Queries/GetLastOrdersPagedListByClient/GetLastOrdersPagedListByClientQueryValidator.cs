using System;
using FluentValidation;


namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class GetLastOrdersPagedListByClientQueryValidator : AbstractValidator<GetLastOrdersPagedListByClientQuery>
    {
        public GetLastOrdersPagedListByClientQueryValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PageSize)
                .NotEmpty();
        }
    }
}
