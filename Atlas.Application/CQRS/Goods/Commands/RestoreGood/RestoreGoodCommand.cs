using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Commands.RestoreGood
{
    public class RestoreGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
