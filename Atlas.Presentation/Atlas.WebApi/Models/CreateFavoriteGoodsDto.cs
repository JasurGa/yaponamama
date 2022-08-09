using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.FavoriteGoods.Commands.CreateManyFavoriteGoods;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class CreateFavoriteGoodsDto : IMapWith<CreateFavoriteGoodsCommand>
    {
        public IList<Guid> GoodIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFavoriteGoodsDto, CreateFavoriteGoodsCommand>()
                .ForMember(dst => dst.GoodIds, opt =>
                    opt.MapFrom(src => src.GoodIds));
        }
    }
}
