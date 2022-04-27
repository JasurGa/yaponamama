using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Promos.Commands.UpdatePromo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Models
{
    public class UpdatePromoDto : IMapWith<UpdatePromoCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePromoDto, UpdatePromoCommand>()
                .ForMember(p => p.Id, opt =>
                    opt.MapFrom(p => p.Id))
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.DiscountPrice, opt =>
                    opt.MapFrom(p => p.DiscountPrice))
                .ForMember(p => p.DiscountPercent, opt =>
                    opt.MapFrom(p => p.DiscountPercent));
        }
    }
}
