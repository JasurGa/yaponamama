using System;
using FluentValidation;

namespace Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails
{
    public class GetOrderFeedbackDetailsQueryValidator : AbstractValidator<GetOrderFeedbackDetailsQuery>
    {
        public GetOrderFeedbackDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
