using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Atlas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;
using Atlas.Domain;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetVerificationRequestDetails
{
    public class GetVerificationRequestDetailsQueryHandler : IRequestHandler<GetVerificationRequestDetailsQuery,
        VerificationRequestDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVerificationRequestDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VerificationRequestDetailsVm> Handle(GetVerificationRequestDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var verificationRequest = await _dbContext.VerificationRequests.Include(x => x.Client.User).FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (verificationRequest == null)
            {
                throw new NotFoundException(nameof(VerificationRequest), request.Id);
            }

            return _mapper.Map<VerificationRequest,
                VerificationRequestDetailsVm>(verificationRequest);
        }
    }
}

