using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoryToGood
{
    public class CreatePromoCategoryToGoodCommand : IRequest<Guid>
    {
        public Guid GoodId { get; set; }

        public Guid PromoCategoryId { get; set; }
    }
}

