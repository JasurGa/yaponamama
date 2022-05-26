using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Commands.DeleteProvider
{
    public class DeleteProviderCommandValidator : AbstractValidator<DeleteProviderCommand>
    {
        public DeleteProviderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
