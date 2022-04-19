using System;
using Atlas.Domain;
using AutoMapper;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GetGoodDetailsQuery : IRequest<GoodDetailsVm>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Good, GetGoodDetailsQuery>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt =>
                    opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath))
                .ForMember(dest => dest.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice));
        }
    }
}
