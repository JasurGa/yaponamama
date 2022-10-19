using System;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetVerificationRequestDetails;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequestDetails
{
    public class GetMyVerificationRequestDetailsQuery : IRequest<VerificationRequestDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}

