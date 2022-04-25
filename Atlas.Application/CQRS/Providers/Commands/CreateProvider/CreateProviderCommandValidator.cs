using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Commands.CreateProvider
{
    public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
    {
        public CreateProviderCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Longitude)
                .NotEmpty();

            RuleFor(p => p.Latitude)
                .NotEmpty();

            RuleFor(p => p.Longitude)
                .NotEmpty();
        }
    }
}
