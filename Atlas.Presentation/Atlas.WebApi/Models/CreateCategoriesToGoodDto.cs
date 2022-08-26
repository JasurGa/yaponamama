using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoriesToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoriesToGoodDto : IMapWith<CreateCategoriesToGoodCommand>
    {
        public Guid GoodId { get; set; }

        public List<Guid> CategoryIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoriesToGoodDto, CreateCategoriesToGoodCommand>()
                .ForMember(p => p.GoodId, opt =>
                    opt.MapFrom(p => p.GoodId))
                .ForMember(p => p.CategoryIds, opt =>
                    opt.MapFrom(p => p.CategoryIds));
        }
    }
}
