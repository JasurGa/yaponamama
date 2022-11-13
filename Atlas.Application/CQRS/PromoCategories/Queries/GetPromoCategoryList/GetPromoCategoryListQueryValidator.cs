using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList
{
    public class GetPromoCategoryListQueryValidator : AbstractValidator<GetPromoCategoryListQuery>
    {
        public GetPromoCategoryListQueryValidator()
        {
        }
    }
}

