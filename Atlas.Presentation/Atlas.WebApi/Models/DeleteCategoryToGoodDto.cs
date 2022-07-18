using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class DeleteCategoryToGoodDto : IMapWith<DeleteCategoryToGoodCommand>
    {
        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCategoryToGoodDto, DeleteCategoryToGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.CategoryId, opt =>
                    opt.MapFrom(src => src.CategoryId));
        }
    }
}
