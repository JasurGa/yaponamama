using System;
using FluentValidation;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests
{
    public class GetMyVerificationRequestsQueryValidator :
        AbstractValidator<GetMyVerificationRequestsQuery>
    {
        public GetMyVerificationRequestsQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

