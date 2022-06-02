using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GeneralCategories.Commands.CreateGeneralCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateGeneralCategoryDto : IMapWith<CreateGeneralCategoryCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGeneralCategoryDto, CreateGeneralCategoryCommand>()
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name));
        }
    }
}
