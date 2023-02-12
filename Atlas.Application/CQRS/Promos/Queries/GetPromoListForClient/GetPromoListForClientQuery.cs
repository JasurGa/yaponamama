using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListForClient
{
    public class GetPromoListForClientQuery : IRequest<PromoListVm>
    {
        public Guid ClientId { get; set; }
    }
}
