using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class GetBotOrdersPagedListQueryValidator : AbstractValidator<GetBotOrdersPagedListQuery>
    {
        public GetBotOrdersPagedListQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

        }
    }
}
