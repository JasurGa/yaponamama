using System;
using FluentValidation;

namespace Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails
{
    public class GetOrderFeedbackDetailsQueryValidator : AbstractValidator<GetOrderFeedbackDetailsQuery>
    {
        public GetOrderFeedbackDetailsQueryValidator()
        {
            RuleFor(of => of.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
