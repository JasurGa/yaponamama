using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PageVisits.Commands.IncrementPageVisit
{
    public class IncrementPageVisitCommandValidator :
        AbstractValidator<IncrementPageVisitCommand>
    {
        public IncrementPageVisitCommandValidator()
        {
            RuleFor(x => x.Path)
                .NotEmpty();
        }
    }
}
