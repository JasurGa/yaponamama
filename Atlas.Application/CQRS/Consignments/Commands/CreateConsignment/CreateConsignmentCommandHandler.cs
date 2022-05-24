﻿using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandHandler : IRequestHandler<CreateConsignmentCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateConsignmentCommand request, CancellationToken cancellationToken)
        {
            var consignment = new Consignment
            {
                Id              = Guid.NewGuid(),
                PurchasedAt     = request.PurchasedAt,
                ExpirateAt      = request.ExpirateAt,
                ShelfLocation   = request.ShelfLocation,
            };

            await _dbContext.Consignments.AddAsync(consignment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return consignment.Id;
        }
    }
}