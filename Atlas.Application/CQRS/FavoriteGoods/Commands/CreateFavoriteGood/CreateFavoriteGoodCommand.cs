using System;
using MediatR;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGood
{
    public class CreateFavoriteGoodCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public Guid GoodId { get; set; }
    }
}
