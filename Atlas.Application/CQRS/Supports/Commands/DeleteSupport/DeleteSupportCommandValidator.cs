using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.DeleteSupport
{
    public class DeleteSupportCommandValidator : AbstractValidator<DeleteSupportCommand>
    {
        public DeleteSupportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
