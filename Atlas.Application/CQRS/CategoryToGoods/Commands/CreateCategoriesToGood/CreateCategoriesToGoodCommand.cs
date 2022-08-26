using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoriesToGood
{
    public class CreateCategoriesToGoodCommand : IRequest
    {
        public Guid GoodId { get; set; }

        public List<Guid> CategoryIds { get; set; }
    }
}