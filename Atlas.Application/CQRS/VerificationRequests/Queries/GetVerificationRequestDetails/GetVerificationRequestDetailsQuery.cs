using System;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetVerificationRequestDetails
{
    public class GetVerificationRequestDetailsQuery : IRequest<VerificationRequestDetailsVm>
    {
        public Guid Id { get; set; }
    }
}

