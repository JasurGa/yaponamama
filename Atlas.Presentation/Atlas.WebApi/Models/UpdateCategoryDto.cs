using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.UpdateCategory;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public int OrderNumber { get; set; }

        public bool IsHidden { get; set; }

        public bool IsVerified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.NameRu, opt =>
                    opt.MapFrom(x => x.NameRu))
                .ForMember(x => x.NameEn, opt =>
                    opt.MapFrom(x => x.NameEn))
                .ForMember(x => x.NameUz, opt =>
                    opt.MapFrom(x => x.NameUz))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x => x.ImageUrl))
                .ForMember(x => x.IsMainCategory, opt =>
                    opt.MapFrom(x => x.IsMainCategory))
                .ForMember(x => x.OrderNumber, opt =>
                    opt.MapFrom(x => x.OrderNumber))
                .ForMember(x => x.IsHidden, opt =>
                    opt.MapFrom(x => x.IsHidden))
                .ForMember(p => p.IsVerified, opt =>
                    opt.MapFrom(p => p.IsVerified));
        }
    }
}
