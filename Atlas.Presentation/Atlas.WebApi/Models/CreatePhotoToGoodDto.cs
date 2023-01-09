using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PhotoToGoods.Commands.CreatePhotoToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
	public class CreatePhotoToGoodDto : IMapWith<CreatePhotoToGoodCommand>
	{
        public Guid GoodId { get; set; }

        public string PhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePhotoToGoodDto, CreatePhotoToGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath));
        }
    }
}

