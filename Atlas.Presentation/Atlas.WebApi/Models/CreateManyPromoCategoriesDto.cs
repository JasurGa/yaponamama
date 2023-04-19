using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategories.Commands.CreateManyPromoCategories;
using AutoMapper;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class CreateManyPromoCategoriesDto : IMapWith<CreateManyPromoCategoriesCommand>
    {
        public List<CreatePromoCategoryLookupDto> PromoCategories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateManyPromoCategoriesDto, CreateManyPromoCategoriesCommand>()
                .ForMember(dst => dst.PromoCategories, opt =>
                    opt.MapFrom(src => src.PromoCategories));
        }
    }
}
