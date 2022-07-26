using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.ImageUrl, opt =>
                    opt.MapFrom(p => ImageUrl))
                .ForMember(p => p.IsMainCategory, opt =>
                    opt.MapFrom(p => IsMainCategory));
        }
    }
}
