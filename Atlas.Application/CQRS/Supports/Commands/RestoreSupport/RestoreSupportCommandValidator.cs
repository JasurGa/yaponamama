using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.RestoreSupport
{
    public class RestoreSupportCommandValidator : AbstractValidator<RestoreSupportCommand>
    {
        public RestoreSupportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
