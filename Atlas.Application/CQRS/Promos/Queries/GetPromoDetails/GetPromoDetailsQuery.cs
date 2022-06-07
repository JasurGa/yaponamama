using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class GetPromoDetailsQuery : IRequest<PromoDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
