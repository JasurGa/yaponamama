﻿using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails
{
    public class GetConsignmentDetailsQueryHandler : IRequestHandler<GetConsignmentDetailsQuery, ConsignmentDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetConsignmentDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ConsignmentDetailsVm> Handle(GetConsignmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var consignment = await _dbContext.Consignments
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (consignment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            return _mapper.Map<ConsignmentDetailsVm>(consignment);
        }
    }
}
