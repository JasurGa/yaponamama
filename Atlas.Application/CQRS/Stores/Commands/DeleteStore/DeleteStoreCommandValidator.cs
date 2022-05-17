using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommandValidator : AbstractValidator<DeleteStoreCommand>
    {
        public DeleteStoreCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEqual(Guid.NewGuid());
        }
    }
}
