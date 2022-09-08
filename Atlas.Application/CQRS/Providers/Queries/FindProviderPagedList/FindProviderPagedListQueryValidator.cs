using System;
using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Models;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Queries.FindProviderPagedList
{
    public class FindProviderPagedListQueryValidator : AbstractValidator<FindProviderPagedListQuery>
    {
        public FindProviderPagedListQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
                .NotNull();

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}

