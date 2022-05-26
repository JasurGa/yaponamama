using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood
{
    public class DeleteStoreToGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
