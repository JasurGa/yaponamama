using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchCategoriesByGoodNameQueryValidator : AbstractValidator<SearchCategoriesByGoodNameQuery>
    {
        public SearchCategoriesByGoodNameQueryValidator()
        {
            RuleFor(e => e.SearchQuery)
                .NotNull();
        }
    }
}

