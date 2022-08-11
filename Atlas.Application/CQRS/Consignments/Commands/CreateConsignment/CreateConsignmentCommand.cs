using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommand : IRequest<Guid>
    {
        public CreateStoreToGoodCommand StoreToGood { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }
    }
}
