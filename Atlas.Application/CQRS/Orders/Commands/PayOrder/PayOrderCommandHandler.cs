using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Atlas.SubscribeApi.Abstractions;
using Atlas.SubscribeApi.Enums;
using Atlas.SubscribeApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Commands.PayOrder
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, SuccessDetailsVm>
    {
        private readonly IAtlasDbContext  _dbContext;
        private readonly ISubscribeClient _subscribeClient;

        public PayOrderCommandHandler(IAtlasDbContext  dbContext, ISubscribeClient subscribeClient) =>
            (_dbContext, _subscribeClient) = (dbContext, subscribeClient);

        public async Task<SuccessDetailsVm> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var order = await _dbContext.Orders.Include(x => x.GoodToOrders)
                .ThenInclude(x => x.Good).FirstOrDefaultAsync(x => 
                    x.Id == request.OrderId, cancellationToken);

            if (order == null || order.ClientId != request.ClientId) 
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var amount = (long)(order.ShippingPrice + order.SellingPrice) * 100;
            var receipt = _subscribeClient.ReceiptsCreate(amount, new AccountLookupDto
                {
                    order_id = order.Id,
                    order    = order.Id,
                },
                "OQ-OT: Оплата заказа",
                new DetailLookupDto
                {
                    receipt_type = (int)ReceiptTypes.Fiscal,
                    shipping = new InnerShippingDetailsVm
                    {
                        title = "Доставка",
                        price = (long)order.ShippingPrice * 100
                    },
                    items = order.GoodToOrders.Select(x => new InnerItemDetailsVm
                    {
                        title        = x.Good.NameRu,
                        price        = x.Good.SellingPrice * 100,
                        count        = x.Count,
                        code         = x.Good.CodeIkpu,
                        vat_percent  = x.Good.SaleTaxPercent,
                        package_code = x.Good.PackageCode
                    }).ToList()
                });

            if (receipt == null)
            {
                return new SuccessDetailsVm
                {
                    success = false,
                    message = "Unable to create a receipt."
                };
            }

            var payment = _subscribeClient.ReceiptsPay(receipt.receipt._id, request.Token, new PayerLookupDto
            {
                phone = order.Client.PhoneNumber
            });

            if (payment == null)
            {
                _subscribeClient.ReceiptsCancel(receipt.receipt._id);
                return new SuccessDetailsVm
                {
                    success = false,
                    message = "Unable to process a payment."
                };
            }

            order.IsPrePayed = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var phoneNumber = client.User.Login.Replace(" ", "").Replace("+", "");
            _subscribeClient.ReceiptsSend(receipt.receipt._id, phoneNumber);

            return new SuccessDetailsVm 
            { 
                success = true 
            };
        }
    }
}
