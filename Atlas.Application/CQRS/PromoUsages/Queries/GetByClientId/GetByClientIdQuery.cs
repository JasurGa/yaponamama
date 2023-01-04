using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetByClientId
{
    public class GetByClientIdQuery : IRequest<PromoUsageListVm>
    {
        public Guid ClientId { get; set; }
    }
}

