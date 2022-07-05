using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodCounts
{
    public class GetGoodCountsQueryValidator : AbstractValidator<GetGoodCountsQuery>
    {
        public GetGoodCountsQueryValidator()
        {
            RuleFor(x => x.CategoryId);
        }
    }
}
