using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories
{
    public class GetGeneralCategoriesQueryValidator
        : AbstractValidator<GetGeneralCategoriesQuery>
    {
        public GetGeneralCategoriesQueryValidator()
        {
        }
    }
}
