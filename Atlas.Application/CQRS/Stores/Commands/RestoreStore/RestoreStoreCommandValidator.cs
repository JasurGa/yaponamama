using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Commands.RestoreStore
{
    public class RestoreStoreCommandValidator : AbstractValidator<RestoreStoreCommand>
    {
        public RestoreStoreCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
