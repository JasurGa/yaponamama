using System;
using System.Collections.Generic;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoriesToGoodDto
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
