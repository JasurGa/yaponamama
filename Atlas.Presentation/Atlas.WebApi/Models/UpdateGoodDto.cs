using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Commands.UpdateGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateGoodDto : IMapWith<UpdateGoodCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public float Mass { get; set; }

        public float Volume { get; set; }

        public float Discount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGoodDto, UpdateGoodCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt =>
                    opt.MapFrom(x => x.Description))
                .ForMember(x => x.PhotoPath, opt =>
                    opt.MapFrom(x => x.PhotoPath))
                .ForMember(x => x.SellingPrice, opt =>
                    opt.MapFrom(x => x.SellingPrice))
                .ForMember(x => x.PurchasePrice, opt =>
                    opt.MapFrom(x => x.PurchasePrice))
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.Mass, opt =>
                    opt.MapFrom(x => x.Mass))
                .ForMember(x => x.Volume, opt =>
                    opt.MapFrom(x => x.Volume))
                .ForMember(x => x.Discount, opt =>
                    opt.MapFrom(x => x.Discount));
        }
    }
}
