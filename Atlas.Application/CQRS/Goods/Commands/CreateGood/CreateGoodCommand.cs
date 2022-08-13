using MediatR;
using System;

namespace Atlas.Application.CQRS.Goods.Commands.CreateGood
{
    public class CreateGoodCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public float Volume { get; set; }

        public float Mass { get; set; }

        public float Discount { get; set; }
    }
}
