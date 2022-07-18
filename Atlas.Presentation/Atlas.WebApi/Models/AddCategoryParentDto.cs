using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.AddCategoryParent;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class AddCategoryParentDto : IMapWith<AddCategoryParentCommand>
    {
        public Guid CategoryId { get; set; }

        public Guid ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddCategoryParentDto, AddCategoryParentCommand>()
                .ForMember(dst => dst.CategoryId, opt => opt.MapFrom(src =>
                    src.CategoryId))
                .ForMember(dst => dst.ParentId, opt => opt.MapFrom(src =>
                    src.ParentId));
        }
    }
}
