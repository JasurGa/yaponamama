using System;
using Atlas.Application.CQRS.PromoUsages.Queries.GetByClientId;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetLastUsagesPaged
{
    public class GetLastUsagesPagedQuery : IRequest<PageDto<PromoUsageLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

