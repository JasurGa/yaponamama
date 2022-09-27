using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.FindGoodPagedList
{
    public class FindGoodPagedListQueryValidator : AbstractValidator<FindGoodPagedListQuery>
    {
        public FindGoodPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

