using FluentValidation;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.DeleteDisposeToConsignment
{
    public class DeleteDisposeToConsignmentCommandValidator : AbstractValidator<DeleteDisposeToConsignmentCommand>
    {
        public DeleteDisposeToConsignmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
