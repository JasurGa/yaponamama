using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Commands.UpdateProvider
{
    public class UpdateProviderCommandValidator : AbstractValidator<UpdateProviderCommand>
    {
        public UpdateProviderCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);

            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Longitude)
                .NotEmpty();

            RuleFor(p => p.Latitude)
                .NotEmpty();

            RuleFor(p => p.Description)
                .NotEmpty();

            RuleFor(p => p.Address)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(p => p.LogotypePath)
                .NotEmpty();
        }
    }
}
