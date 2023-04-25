using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePromoCategoriesToGoodDto : IMapWith<CreatePromoCategoriesToGoodCommand>
    {
        public List<Guid> GoodIds { get; set; }

        public Guid PromoCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoCategoriesToGoodDto, CreatePromoCategoriesToGoodCommand>()
                .ForMember(dst => dst.GoodIds, opt =>
                    opt.MapFrom(src => src.GoodIds))
                .ForMember(dst => dst.PromoCategoryId, opt =>
                    opt.MapFrom(src => src.PromoCategoryId));
        }
    }
}

