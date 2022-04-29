using MediatR;
using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
