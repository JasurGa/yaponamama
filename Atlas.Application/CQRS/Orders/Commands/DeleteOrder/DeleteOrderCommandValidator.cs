﻿using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotEqual(Guid.NewGuid());
        }
    }
}
