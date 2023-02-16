using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.UpdatePromoToGood
{
    public class UpdatePromoToGoodCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid PromoId { get; set; }

        public Guid GoodId { get; set; }
    }
}
