﻿using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Commands.UpdateGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateGoodDto : IMapWith<UpdateGoodCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string NoteRu { get; set; }

        public string NoteEn { get; set; }

        public string NoteUz { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public float Mass { get; set; }

        public float Volume { get; set; }

        public float Discount { get; set; }

        public string CodeIkpu { get; set; }

        public int SaleTaxPercent { get; set; }

        public string PackageCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGoodDto, UpdateGoodCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.NameRu, opt =>
                    opt.MapFrom(x => x.NameRu))
                .ForMember(x => x.NameEn, opt =>
                    opt.MapFrom(x => x.NameEn))
                .ForMember(x => x.NameUz, opt =>
                    opt.MapFrom(x => x.NameUz))
                .ForMember(x => x.Description, opt =>
                    opt.MapFrom(x => x.Description))
                .ForMember(x => x.DescriptionRu, opt =>
                    opt.MapFrom(x => x.DescriptionRu))
                .ForMember(x => x.DescriptionEn, opt =>
                    opt.MapFrom(x => x.DescriptionEn))
                .ForMember(x => x.DescriptionUz, opt =>
                    opt.MapFrom(x => x.DescriptionUz))
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
                    opt.MapFrom(x => x.Discount))
                .ForMember(x => x.CodeIkpu, opt =>
                    opt.MapFrom(x => x.CodeIkpu))
                .ForMember(x => x.SaleTaxPercent, opt =>
                    opt.MapFrom(x => x.SaleTaxPercent))
                .ForMember(x => x.PackageCode, opt =>
                    opt.MapFrom(x => x.PackageCode));
        }
    }
}
