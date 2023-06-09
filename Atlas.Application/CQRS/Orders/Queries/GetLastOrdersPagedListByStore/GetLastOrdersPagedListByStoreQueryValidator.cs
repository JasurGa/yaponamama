﻿using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByStore
{
    public class GetLastOrdersPagedListByStoreQueryValidator :
        AbstractValidator<GetLastOrdersPagedListByStoreQuery>
    {
        public GetLastOrdersPagedListByStoreQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

        }
    }
}
