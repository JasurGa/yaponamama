using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage
{
    public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateChatMessageCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var fromUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.FromUserId,
                cancellationToken);

            if (fromUser == null)
            {
                throw new NotFoundException(nameof(User), request.FromUserId);
            }

            var toUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.ToUserId,
                cancellationToken);

            if (toUser == null)
            {
                throw new NotFoundException(nameof(User), request.ToUserId);
            }

            var chatMessage = new ChatMessage
            {
                Id          = Guid.NewGuid(),
                FromUserId  = request.FromUserId,
                ToUserId    = request.ToUserId,
                Body        = request.Body,
                Optional    = request.Optional,
                MessageType = request.MessageType,
                CreatedAt   = DateTime.UtcNow,
            };

            await _dbContext.ChatMessages.AddAsync(chatMessage,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return chatMessage.Id;
        }
    }
}
