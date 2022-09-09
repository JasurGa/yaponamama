﻿using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.FindOrderPagedList
{
    public class FindOrderPagedListQueryValidator : AbstractValidator<FindOrderPagedListQuery>
    {
        public FindOrderPagedListQueryValidator()
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

