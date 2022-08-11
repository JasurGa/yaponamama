using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatMessagesForUser
{
    public class GetChatMessagesForUserQueryHandler : IRequestHandler<GetChatMessagesForUserQuery,
        PageDto<ChatMessageLookupDto>>
    {
        private readonly IMapper         _mapper; 
        private readonly IAtlasDbContext _dbContext;

        public GetChatMessagesForUserQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ChatMessageLookupDto>> Handle(GetChatMessagesForUserQuery request,
            CancellationToken cancellationToken)
        {
            var messagesCount = await _dbContext.ChatMessages.CountAsync(x =>
                (x.FromUserId == request.ChatUserId && x.ToUserId == request.MyUserId) ||
                (x.FromUserId == request.MyUserId   && x.ToUserId == request.ChatUserId),
                cancellationToken);

            var pagesCount = (int)Math.Ceiling((double)messagesCount /
                    request.PageSize);

            if (pagesCount <= request.PageIndex)
            {
                throw new NotFoundException(nameof(PageDto<ChatMessageLookupDto>), request.PageIndex);
            }

            var messages = await _dbContext.ChatMessages.Where(x =>
                (x.FromUserId == request.ChatUserId && x.ToUserId == request.MyUserId) ||
                (x.FromUserId == request.MyUserId   && x.ToUserId == request.ChatUserId))
                .OrderBy(x => x.CreatedAt)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<ChatMessageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ChatMessageLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = messagesCount,
                PageCount  = pagesCount,
                Data       = messages                
            };
        }
    }
}
