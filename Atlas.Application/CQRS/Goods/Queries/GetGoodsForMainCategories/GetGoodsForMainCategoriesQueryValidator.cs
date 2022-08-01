using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories
{
    public class GetGoodsForMainCategoriesQueryValidator :
        AbstractValidator<GetGoodsForMainCategoriesQuery>
    {
        public GetGoodsForMainCategoriesQueryValidator()
        {
        }
    }
}
