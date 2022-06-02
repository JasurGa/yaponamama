using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GeneralCategories.Commands.UpdateGeneralCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateGeneralCategoryDto : IMapWith<UpdateGeneralCategoryCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGeneralCategoryDto, UpdateGeneralCategoryCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name));
        }
    }
}
