using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.RemoveCategoryParent;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class RemoveCategoryParentDto: IMapWith<RemoveCategoryParentCommand>
    {
        public Guid CategoryId { get; set; }

        public Guid ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RemoveCategoryParentDto, RemoveCategoryParentCommand>()
                .ForMember(dst => dst.CategoryId, opt => opt.MapFrom(src =>
                    src.CategoryId))
                .ForMember(dst => dst.ParentId, opt => opt.MapFrom(src =>
                    src.ParentId));
        }
    }
}
