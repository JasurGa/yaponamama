using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetVerificationRequestDetails;
using MediatR;
using AutoMapper;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequestDetails
{
    public class GetMyVerificationRequestDetailsQueryHandler : IRequestHandler<GetMyVerificationRequestDetailsQuery,
        VerificationRequestDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetMyVerificationRequestDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VerificationRequestDetailsVm> Handle(GetMyVerificationRequestDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var verificationRequest = await _dbContext.VerificationRequests.Include(x => x.Client.User).FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (verificationRequest == null || verificationRequest.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(VerificationRequest), request.Id);
            }

            return _mapper.Map<VerificationRequest,
                VerificationRequestDetailsVm>(verificationRequest);
        }
    }
}

