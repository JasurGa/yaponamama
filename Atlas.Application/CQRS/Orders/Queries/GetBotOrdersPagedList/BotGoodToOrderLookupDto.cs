using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class BotGoodToOrderLookupDto : IMapWith<GoodToOrder>
    {
        public Guid Id { get; set; }

        public string PhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GoodToOrder, BotGoodToOrderLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PhotoPath, opt =>
                    opt.MapFrom(src => src.Good.PhotoPath));
        }
    }
}
