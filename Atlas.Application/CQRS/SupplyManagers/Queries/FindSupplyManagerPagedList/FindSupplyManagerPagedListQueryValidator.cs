using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.FindSupplyManagerPagedList
{
    public class FindSupplyManagerPagedListQueryValidator : AbstractValidator<FindSupplyManagerPagedListQuery>
    {
        public FindSupplyManagerPagedListQueryValidator()
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

