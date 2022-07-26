using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListByClientId
{
    public class GetPromoListByClientIdQuery : IRequest<PromoListVm>
    {
        public Guid ClientId { get; set; }
    }
}
