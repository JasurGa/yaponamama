using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class GetOrderCommentListByOrderQueryHandler : IRequestHandler<GetOrderCommentListByOrderQuery, OrderCommentListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderCommentListByOrderQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OrderCommentListVm> Handle(GetOrderCommentListByOrderQuery request, CancellationToken cancellationToken)
        {
            var orderComments = await _dbContext.OrderComments
                .Where(x => x.OrderId == request.OrderId)
                .ProjectTo<OrderCommentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new OrderCommentListVm { OrderComments = orderComments };
        }
    }
}
