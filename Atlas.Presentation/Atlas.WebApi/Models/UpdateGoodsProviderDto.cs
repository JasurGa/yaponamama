using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Commands.UpdateGoodsProvider;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class UpdateGoodsProviderDto : IMapWith<UpdateGoodsProviderCommand>
    {
        public Guid ProviderId { get; set; }

        public List<Guid> GoodIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGoodsProviderDto, UpdateGoodsProviderCommand>()
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.GoodIds, opt =>
                    opt.MapFrom(x => x.GoodIds));
        }
    }
}
