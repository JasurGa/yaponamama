using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.FindStoresPagedList
{
    public class FindStoresPagedListQueryValidator : AbstractValidator<FindStoresPagedListQuery>
    {
        public FindStoresPagedListQueryValidator()
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

