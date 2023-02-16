using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId
{
    public class GetPromoToGoodsByPromoIdQuery : IRequest<PromoToGoodListVm>
    {
        public Guid PromoId { get; set; }
    }
}
