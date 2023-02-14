﻿using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreDetails
{
    public class StoreDetailsVm : IMapWith<Store>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; }

        public string AddressRu { get; set; }

        public string AddressEn { get; set; }

        public string AddressUz { get; set; }

        public string PhoneNumber { get; set; }

        public TimeSpan WorkStartsAt { get; set; }

        public TimeSpan WorkFinishesAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreDetailsVm>()
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
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude))
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.AddressRu, opt =>
                    opt.MapFrom(x => x.AddressRu))
                .ForMember(x => x.AddressEn, opt =>
                    opt.MapFrom(x => x.AddressEn))
                .ForMember(x => x.AddressUz, opt =>
                    opt.MapFrom(x => x.AddressUz))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.WorkStartsAt, opt =>
                    opt.MapFrom(src => src.WorkStartsAt))
                .ForMember(x => x.WorkFinishesAt, opt =>
                    opt.MapFrom(src => src.WorkFinishesAt));
        }
    }
}
