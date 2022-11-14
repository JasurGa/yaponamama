using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.DeletePromoCategoryToGood
{
    public class DeletePromoCategoryToGoodCommand : IRequest
    {
        public Guid GoodId { get; set; }

        public Guid PromoCategoryId { get; set; }
    }
}

