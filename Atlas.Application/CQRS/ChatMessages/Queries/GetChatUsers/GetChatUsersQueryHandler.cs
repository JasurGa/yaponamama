using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{

    public class GetChatUsersQueryHandler : IRequestHandler<GetChatUsersQuery,
        ChatUsersListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetChatUsersQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public class UnreadCountModel
        {
            public Guid UserId { get; set; }

            public int UnreadCount { get; set; }
        }

        public async Task<ChatUsersListVm> Handle(GetChatUsersQuery request, CancellationToken cancellationToken)
        {
            var resultTo = await _dbContext.ChatMessages.Where(x => x.FromUserId == request.UserId)
                .Select(x => x.ToUserId).Distinct().ToListAsync(cancellationToken);

            var resultFrom = await _dbContext.ChatMessages.Where(x => x.ToUserId == request.UserId)
                .Select(x => x.FromUserId).Distinct().ToListAsync(cancellationToken);

            var unreadCount = await _dbContext.ChatMessages.Where(x => x.ToUserId == request.UserId)
                .GroupBy(x => x.ToUserId).Select((x) => new UnreadCountModel
                {
                    UserId      = x.Key,
                    UnreadCount = x.Count(x => !x.HasBeenRead),
                })
                .ToListAsync(cancellationToken);

            var userIds = resultTo.Concat(resultFrom).Distinct();

            var users = await _dbContext.Users.Where(x => userIds.Contains(x.Id))
                .ProjectTo<ChatUserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                var unread = unreadCount.FirstOrDefault(x => x.UserId == user.UserId);
                if (unread != null)
                {
                    user.UnreadCount = unread.UnreadCount;
                }
            }

            return new ChatUsersListVm
            {
                ChatUsers = users
            };
        }
    }
}
