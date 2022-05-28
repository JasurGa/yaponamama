using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class CategoryToGoodLookupDto : IMapWith<CategoryToGood>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryToGood, CategoryToGoodLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.CategoryId, opt =>
                    opt.MapFrom(src => src.CategoryId));
        }
    }
}
