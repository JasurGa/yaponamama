using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PhotoToGoods.Commands;
using Atlas.Application.CQRS.PhotoToGoods.Commands.CreateManyPhotosToGoods;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateManyPhotosToGoodsDto : IMapWith<CreateManyPhotosToGoodsCommand>
    {
        public List<CreateOnePhotoToGoodDto> PhotoToGoods { get; set; } 

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateManyPhotosToGoodsDto, CreateManyPhotosToGoodsCommand>()
                .ForMember(dst => dst.PhotoToGoods, opt =>
                    opt.MapFrom(src => src.PhotoToGoods));
        }
    }
}
