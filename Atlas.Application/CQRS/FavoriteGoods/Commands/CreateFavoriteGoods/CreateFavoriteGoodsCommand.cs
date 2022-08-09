using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateManyFavoriteGoods
{
    public class CreateFavoriteGoodsCommand : IRequest<List<Guid>>
    {
        public Guid ClientId { get; set; }

        public IList<Guid> GoodIds { get; set; }
    }
}
