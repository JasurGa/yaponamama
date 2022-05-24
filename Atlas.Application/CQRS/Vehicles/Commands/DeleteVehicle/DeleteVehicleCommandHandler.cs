﻿using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteVehicleCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if(vehicle == null)
            {
                throw new NotFoundException(nameof(vehicle), request.Id);
            }

            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}