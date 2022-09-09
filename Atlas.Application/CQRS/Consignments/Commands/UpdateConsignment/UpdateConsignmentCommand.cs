using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment
{
    public class UpdateConsignmentCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public int Count { get; set; }

    }
}
