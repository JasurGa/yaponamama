using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class GetPromoListQuery : IRequest<PromoListVm>
    {

    }
}
