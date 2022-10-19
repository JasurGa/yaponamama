using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests
{
    public class GetMyVerificationRequestsQuery : IRequest<PageDto<VerificationRequestLookupDto>>
    {
        public Guid ClientId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

