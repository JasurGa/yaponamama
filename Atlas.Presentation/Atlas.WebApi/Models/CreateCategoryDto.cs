using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public int OrderNumber { get; set; }
        
        public bool IsHidden { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>()
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.NameRu, opt =>
                    opt.MapFrom(p => p.NameRu))
                .ForMember(p => p.NameEn, opt =>
                    opt.MapFrom(p => p.NameEn))
                .ForMember(p => p.NameUz, opt =>
                    opt.MapFrom(p => p.NameUz))
                .ForMember(p => p.ImageUrl, opt =>
                    opt.MapFrom(p => p.ImageUrl))
                .ForMember(p => p.IsMainCategory, opt =>
                    opt.MapFrom(p => p.IsMainCategory))
                .ForMember(p => p.OrderNumber, opt =>
                    opt.MapFrom(p => p.OrderNumber))
                .ForMember(p => p.IsHidden, opt =>
                    opt.MapFrom(p => p.IsHidden));
        }
    }
}
