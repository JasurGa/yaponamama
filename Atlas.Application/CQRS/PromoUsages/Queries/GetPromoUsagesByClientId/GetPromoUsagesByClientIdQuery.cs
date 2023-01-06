using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId
{
    public class GetPromoUsagesByClientIdQuery : IRequest<PromoUsageListVm>
    {
        public Guid ClientId { get; set; }
    }
}

