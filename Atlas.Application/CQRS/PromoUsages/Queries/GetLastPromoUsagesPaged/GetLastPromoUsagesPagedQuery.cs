using System;
using Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetLastPromoUsagesPaged
{
    public class GetLastPromoUsagesPagedQuery : IRequest<PageDto<PromoUsageLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

