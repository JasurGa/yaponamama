using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Queries.FindCourierPagedList
{
    public class FindCourierPagedListQueryValidator : AbstractValidator<FindCourierPagedListQuery>
    {
        public FindCourierPagedListQueryValidator()
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

