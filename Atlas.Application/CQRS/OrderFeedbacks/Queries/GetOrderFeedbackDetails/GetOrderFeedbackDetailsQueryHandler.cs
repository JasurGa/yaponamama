using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails
{
    public class GetOrderFeedbackDetailsQueryHandler : IRequestHandler<GetOrderFeedbackDetailsQuery, OrderFeedbackDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderFeedbackDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OrderFeedbackDetailsVm> Handle(GetOrderFeedbackDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderFeedback = await _dbContext.OrderFeedbacks.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (orderFeedback == null)
            {
                throw new NotFoundException(nameof(OrderFeedback), request.Id);
            }

            return _mapper.Map<OrderFeedback, OrderFeedbackDetailsVm>(orderFeedback);
        }
    }
}
