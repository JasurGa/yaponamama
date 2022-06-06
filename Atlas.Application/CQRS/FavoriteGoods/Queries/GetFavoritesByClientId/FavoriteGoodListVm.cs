using System.Collections.Generic;

namespace Atlas.Application.CQRS.FavoriteGoods.Queries.GetFavoritesByClientId
{
    public class FavoriteGoodListVm
    {
        public IList<FavoriteGoodLookupDto> FavoriteGoods { get; set; }
    }
}