using System;
using FluentValidation;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetPagedVerificationRequestsList
{
    public class GetPagedVerificationRequestsListQueryValidator : AbstractValidator<GetPagedVerificationRequestsListQuery>
    {
        public GetPagedVerificationRequestsListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

