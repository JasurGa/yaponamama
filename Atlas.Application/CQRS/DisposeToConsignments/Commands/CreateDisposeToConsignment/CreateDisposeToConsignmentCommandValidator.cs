using FluentValidation;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment
{
    public class CreateDisposeToConsignmentCommandValidator : AbstractValidator<CreateDisposeToConsignmentCommand>
    {
        public CreateDisposeToConsignmentCommandValidator()
        {
            RuleFor(x => x.ConsignmentId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Count)
                .NotEmpty();
        }
    }
}
