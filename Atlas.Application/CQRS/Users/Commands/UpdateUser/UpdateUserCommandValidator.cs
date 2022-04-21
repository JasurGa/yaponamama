using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(e => e.Id).NotEqual(Guid.Empty);

            RuleFor(e => e.FirstName).NotEmpty();

            RuleFor(e => e.LastName).NotEmpty();

            RuleFor(e => e.Birthday).NotEmpty();
        }
    }
}
