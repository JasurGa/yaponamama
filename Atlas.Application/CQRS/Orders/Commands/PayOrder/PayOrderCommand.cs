using Atlas.SubscribeApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Commands.PayOrder
{
    public class PayOrderCommand : IRequest<SuccessDetailsVm>
    {
        public Guid ClientId { get; set; }

        public Guid OrderId { get; set; }

        public string Token { get; set; }
    }
}
