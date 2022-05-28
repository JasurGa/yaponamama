using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails
{
    public class GetNotificationDetailsQueryHandler : IRequestHandler<GetNotificationDetailsQuery,
        NotificationDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetNotificationDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<NotificationDetailsVm> Handle(GetNotificationDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (notification == null || notification.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Notification), request.Id);
            }

            return _mapper.Map<NotificationDetailsVm>(notification);
        }
    }
}
