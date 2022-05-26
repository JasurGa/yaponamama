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
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Longitude)
                .NotEmpty();

            RuleFor(x => x.Latitude)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty();
        }
    }
}
