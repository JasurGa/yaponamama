using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Promos.Commands.CreatePromo;
using AutoMapper;

namespace Atlas.WebApi.Models
{

    public class CreatePromoDto : IMapWith<CreatePromoCommand>
    {
        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoDto, CreatePromoCommand>()
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.DiscountPrice, opt =>
                    opt.MapFrom(p => p.DiscountPrice))
                .ForMember(p => p.DiscountPercent, opt =>
                    opt.MapFrom(p => p.DiscountPercent));
        }
    }
}
