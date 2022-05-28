using FluentValidation;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.RestoreSupplyManager
{
    public class RestoreSupplyManagerCommandValidator : AbstractValidator<RestoreSupplyManagerCommand>
    {
        public RestoreSupplyManagerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
