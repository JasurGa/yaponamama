using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood
{
    public class CreateStoreToGoodCommand : IRequest<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public long Count { get; set; }
    }
}
