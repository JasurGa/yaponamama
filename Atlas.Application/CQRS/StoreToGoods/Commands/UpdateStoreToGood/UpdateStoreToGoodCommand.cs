using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood
{
    public class UpdateStoreToGoodCommand : IRequest
    {
        public Guid Id { get; set; }

        public long Count { get; set; }
    }
}
