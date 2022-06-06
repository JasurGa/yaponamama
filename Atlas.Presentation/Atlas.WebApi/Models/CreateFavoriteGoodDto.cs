using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateFavoriteGoodDto : IMapWith<CreateFavoriteGoodCommand>
    {
        public Guid GoodId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFavoriteGoodDto, CreateFavoriteGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId));
        }
    }
}
