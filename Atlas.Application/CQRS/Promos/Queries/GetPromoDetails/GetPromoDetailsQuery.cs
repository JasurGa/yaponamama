using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class GetPromoDetailsQuery : IRequest<PromoDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
