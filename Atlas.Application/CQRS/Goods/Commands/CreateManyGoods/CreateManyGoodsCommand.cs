using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Commands.CreateManyGoods
{
    public class CreateManyGoodsCommand : IRequest<List<Guid>>
    {
        public List<CreateGoodLookupDto> Goods { get; set; }
    }
}
