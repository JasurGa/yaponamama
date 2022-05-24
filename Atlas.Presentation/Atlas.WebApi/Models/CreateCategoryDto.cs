using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name));
        }
    }
}
