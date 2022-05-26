using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Commands.RestoreStore
{
    public class RestoreStoreCommandValidator : AbstractValidator<RestoreStoreCommand>
    {
        public RestoreStoreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
