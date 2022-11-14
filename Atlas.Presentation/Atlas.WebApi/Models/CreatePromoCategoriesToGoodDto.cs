using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePromoCategoriesToGoodDto : IMapWith<CreatePromoCategoriesToGoodCommand>
    {
        public Guid GoodId { get; set; }

        public List<Guid> PromoCategoryIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoCategoriesToGoodDto, CreatePromoCategoriesToGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.PromoCategoryIds, opt =>
                    opt.MapFrom(src => src.PromoCategoryIds));
        }
    }
}

