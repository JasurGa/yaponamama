using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise;
using Atlas.Domain;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetPromoAdvertiseById
{
    public class PromoAdvertiseDetailsVm
    {
        public PromoAdvertisePageListVm PromoAdvertisePages { get; set; }

        public GoodListVm Goods { get; set; }
    }
}