using MediatR;
using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommand : IRequest
    {
        public Guid CategoryId { get; set; }

        public Guid GoodId { get; set; }
    }
}
