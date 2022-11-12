using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise
{
    public class GetPagesByPromoAdvertiseQuery : IRequest<PromoAdvertisePageListVm>
    {
        public Guid PromoAdvertiseId { get; set; }
    }
}

