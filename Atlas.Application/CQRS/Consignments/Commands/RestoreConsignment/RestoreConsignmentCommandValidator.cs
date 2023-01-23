using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.RestoreConsignment
{
    public class RestoreConsignmentCommandValidator : AbstractValidator<RestoreConsignmentCommand>
    {
        public RestoreConsignmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
