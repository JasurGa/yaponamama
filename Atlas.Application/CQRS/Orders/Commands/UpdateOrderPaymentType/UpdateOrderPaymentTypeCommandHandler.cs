using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType
{
    public class UpdateOrderPaymentTypeCommandHandler : IRequestHandler<UpdateOrderPaymentTypeCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateOrderPaymentTypeCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateOrderPaymentTypeCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            order.PaymentType = request.PaymentType;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
