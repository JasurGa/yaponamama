using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryValidator : AbstractValidator<GetCategoryListQuery>
    {
        public GetCategoryListQueryValidator()
        {
        }
    }
}
