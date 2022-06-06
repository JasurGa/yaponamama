using System;
using MediatR;

namespace Atlas.Application.CQRS.FavoriteGoods.Queries.GetFavoritesByClientId
{
    public class GetFavoritesByClientIdQuery : IRequest<FavoriteGoodListVm>
    {
        public Guid ClientId { get; set; }
    }
}
