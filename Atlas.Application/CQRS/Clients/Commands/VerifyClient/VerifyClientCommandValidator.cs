using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Clients.Commands.VerifyClient
{
    public class VerifyClientCommandValidator : AbstractValidator<VerifyClientCommand>
    {
        public VerifyClientCommandValidator() 
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
