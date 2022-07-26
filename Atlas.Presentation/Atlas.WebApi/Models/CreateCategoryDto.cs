using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public string Name { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.IsMainCategory, opt =>
                    opt.MapFrom(x => x.IsMainCategory));
        }
    }
}
