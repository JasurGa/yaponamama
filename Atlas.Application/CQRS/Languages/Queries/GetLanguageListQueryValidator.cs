using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Languages.Queries
{
    public class GetLanguageListQueryValidator : AbstractValidator<GetLanguageListQuery>
    {
        public GetLanguageListQueryValidator()
        {

        }
    }
}
