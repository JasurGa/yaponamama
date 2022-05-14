using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.DeleteConsignment
{
    public class DeleteConsignmentCommandValidator : AbstractValidator<DeleteConsignmentCommand>
    {
        public DeleteConsignmentCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
