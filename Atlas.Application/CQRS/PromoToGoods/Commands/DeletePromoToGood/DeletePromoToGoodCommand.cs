using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.DeletePromoToGood
{
    public class DeletePromoToGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
