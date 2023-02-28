using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGoods
{
    public class DeleteFavoriteGoodsCommand : IRequest<List<Guid>>
    {
        public Guid ClientId { get; set; }

        public IList<Guid> FavoriteGoodIds { get; set; }
    }
}
