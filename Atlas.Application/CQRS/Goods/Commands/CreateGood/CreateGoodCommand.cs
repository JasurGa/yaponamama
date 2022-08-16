using MediatR;
using System;

namespace Atlas.Application.CQRS.Goods.Commands.CreateGood
{
    public class CreateGoodCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public float Volume { get; set; }

        public float Mass { get; set; }

        public float Discount { get; set; }
    }
}
