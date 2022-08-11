using System;
using FluentValidation;

namespace Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage
{
    public class CreateChatMessageCommandValidator : AbstractValidator<CreateChatMessageCommand>
    {
        public CreateChatMessageCommandValidator()
        {
            RuleFor(x => x.FromUserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ToUserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.MessageType)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}
