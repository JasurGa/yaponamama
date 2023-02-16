using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId
{
    public class PromoToGoodLookupDto : IMapWith<PromoToGood>
    {
        public Guid Id { get; set; }

        public Guid PromoId { get; set; }

        public GoodLookupDto Good { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PromoToGood, PromoToGoodLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.PromoId, opt =>
                    opt.MapFrom(src => src.PromoId))
                .ForMember(dst => dst.Good, opt =>
                    opt.MapFrom(src => src.Good));
        }
    }
}
