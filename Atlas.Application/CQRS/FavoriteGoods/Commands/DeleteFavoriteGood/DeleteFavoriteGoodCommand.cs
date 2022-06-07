using System;
using MediatR;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGood
{
    public class DeleteFavoriteGoodCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
