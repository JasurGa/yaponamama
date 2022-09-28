using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrderByClient
{
    public class CancelOrderByClientCommandValidator : AbstractValidator<CancelOrderByClientCommand>
    {
        public CancelOrderByClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}

