using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoryToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePromoCategoryToGoodDto : IMapWith<CreatePromoCategoryToGoodCommand>
    {
        public Guid GoodId { get; set; }

        public Guid PromoCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoCategoryToGoodDto, CreatePromoCategoryToGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.PromoCategoryId, opt =>
                    opt.MapFrom(src => src.PromoCategoryId));
        }
    }
}

