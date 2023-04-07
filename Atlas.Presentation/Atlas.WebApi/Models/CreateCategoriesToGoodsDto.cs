using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoriesToGood;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateManyCategoryToGood;
using AutoMapper;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class CreateCategoriesToGoodsDto : IMapWith<CreateManyCategoryToGoodCommand>
    {
        public List<CategoryToGoodLookupDto> CategoriesToGoods { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoriesToGoodsDto, CreateManyCategoryToGoodCommand>()
                .ForMember(dst => dst.CategoriesToGoods, opt =>
                    opt.MapFrom(src => src.CategoriesToGoods));
        }
    }
}
