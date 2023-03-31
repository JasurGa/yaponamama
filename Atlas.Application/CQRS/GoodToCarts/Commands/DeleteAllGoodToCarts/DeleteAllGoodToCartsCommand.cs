using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteAllGoodToCarts
{
    public class DeleteAllGoodToCartsCommand : IRequest
    {
        public Guid ClientId { get; set; }
    }
}
