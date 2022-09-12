using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.FindVehiclesPagedList
{
    public class FindVehiclesPagedListQueryValidator : AbstractValidator<FindVehiclesPagedListQuery>
    {
        public FindVehiclesPagedListQueryValidator()
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

