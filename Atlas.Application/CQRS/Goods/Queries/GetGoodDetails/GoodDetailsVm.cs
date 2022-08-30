using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GoodDetailsVm : IMapWith<Good>
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string ProviderName { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string PhotoPath { get; set; }

        public long PurchasePrice { get; set; }

        public long SellingPrice { get; set; }

        public float Mass { get; set; }

        public float Volume { get; set; }

        public int Discount { get; set; }

        public bool IsDeleted { get; set; }

        public List<CategoryLookupDto> Categories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Good, GoodDetailsVm>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dest => dest.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz))
                .ForMember(dest => dest.Description, opt =>
                    opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DescriptionRu, opt =>
                    opt.MapFrom(src => src.DescriptionRu))
                .ForMember(dest => dest.DescriptionEn, opt =>
                    opt.MapFrom(src => src.DescriptionEn))
                .ForMember(dest => dest.DescriptionUz, opt =>
                    opt.MapFrom(src => src.DescriptionUz))
                .ForMember(dest => dest.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath))
                .ForMember(dest => dest.PurchasePrice, opt =>
                    opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dest => dest.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dest => dest.Mass, opt =>
                    opt.MapFrom(src => src.Mass))
                .ForMember(dest => dest.Volume, opt =>
                    opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.ProviderId, opt =>
                    opt.MapFrom(src => src.Provider.Id))
                .ForMember(dest => dest.ProviderName, opt =>
                    opt.MapFrom(src => src.Provider.Name))
                .ForMember(dest => dest.Discount, opt =>
                    opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.IsDeleted, opt =>
                    opt.MapFrom(src => src.IsDeleted));
        }
    }
}
