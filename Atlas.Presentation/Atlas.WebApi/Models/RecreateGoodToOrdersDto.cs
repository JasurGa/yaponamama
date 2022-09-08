using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Commands.RecreateGoodToOrders;
using AutoMapper;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class RecreateGoodToOrdersDto : IMapWith<RecreateGoodToOrdersCommand>
    {
        public IList<CreateGoodToOrderDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RecreateGoodToOrdersDto, RecreateGoodToOrdersCommand>()
                .ForMember(x => x.GoodToOrders, opt =>
                    opt.MapFrom(x => x.GoodToOrders));
        }
    }
}
