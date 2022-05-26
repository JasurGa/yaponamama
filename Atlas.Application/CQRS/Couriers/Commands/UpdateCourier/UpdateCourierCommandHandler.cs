using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;


namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommandHandler : IRequestHandler<UpdateCourierCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateCourierCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCourierCommand request,
            CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if(courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }
        
            courier.PassportPhotoPath = request.PassportPhotoPath;
            courier.DriverLicensePath = request.DriverLicensePath;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
