using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForPromoCategories
{
    public class GetGoodsForPromoCategoriesQueryValidator : AbstractValidator<GetGoodsForPromoCategoriesQuery>
    {
        public GetGoodsForPromoCategoriesQueryValidator()
        {
        }
    }
}

