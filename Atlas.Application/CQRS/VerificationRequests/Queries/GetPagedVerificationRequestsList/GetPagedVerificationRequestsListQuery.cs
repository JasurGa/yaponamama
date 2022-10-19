using System;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetPagedVerificationRequestsList
{
    public class GetPagedVerificationRequestsListQuery : IRequest<PageDto<VerificationRequestLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

