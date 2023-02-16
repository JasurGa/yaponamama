using Atlas.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood
{
    public class CreatePromoToGoodCommand : IRequest<Guid>
    {
        public Guid PromoId { get; set; }

        public Guid GoodId { get; set; }
    }
}
