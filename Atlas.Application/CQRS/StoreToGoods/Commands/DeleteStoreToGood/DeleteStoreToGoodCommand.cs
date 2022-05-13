using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood
{
    public class DeleteStoreToGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
