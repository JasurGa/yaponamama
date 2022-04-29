using MediatR;
using System;


namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood
{
    public class CreateCategoryToGoodCommand : IRequest<Guid>
    {
        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
