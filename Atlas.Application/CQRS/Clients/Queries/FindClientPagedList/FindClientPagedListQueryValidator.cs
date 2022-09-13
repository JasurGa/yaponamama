using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Clients.Queries.FindClientPagedList
{
    public class FindClientPagedListQueryValidator : AbstractValidator<FindClientPagedListQuery>
    {
        public FindClientPagedListQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
                .NotNull();

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

