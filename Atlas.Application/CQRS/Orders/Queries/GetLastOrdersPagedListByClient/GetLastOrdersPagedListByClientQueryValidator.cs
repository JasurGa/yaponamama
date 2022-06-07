using System;
using FluentValidation;


namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class GetLastOrdersPagedListByClientQueryValidator : AbstractValidator<GetLastOrdersPagedListByClientQuery>
    {
        public GetLastOrdersPagedListByClientQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
