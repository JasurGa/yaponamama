using System;
using FluentValidation;

namespace Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList
{
    public class FindHeadRecruitersPagedListQueryValidator : AbstractValidator<FindHeadRecruitersPagedListQuery>
    {
        public FindHeadRecruitersPagedListQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
                .NotNull();

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

