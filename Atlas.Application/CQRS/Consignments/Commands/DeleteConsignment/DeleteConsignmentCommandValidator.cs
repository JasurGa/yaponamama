using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Commands.DeleteConsignment
{
    public class DeleteConsignmentCommandValidator : AbstractValidator<DeleteConsignmentCommand>
    {
        public DeleteConsignmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
