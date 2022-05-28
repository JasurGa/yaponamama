using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.DeleteSupplyManager
{
    public class DeleteSupplyManagerCommandValidator : AbstractValidator<DeleteSupplyManagerCommand>
    {
        public DeleteSupplyManagerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
