using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Commands.CreateManyGoods;
using AutoMapper;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class CreateManyGoodsDto : IMapWith<CreateManyGoodsCommand>
    {
        public List<CreateGoodLookupDto> Goods { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateManyGoodsDto, CreateManyGoodsCommand>()
                .ForMember(dst => dst.Goods, opt =>
                    opt.MapFrom(src => src.Goods));
        }
    }
}
