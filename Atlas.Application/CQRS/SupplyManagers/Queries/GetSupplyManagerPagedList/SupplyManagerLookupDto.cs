﻿using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList
{
    public class SupplyManagerLookupDto : IMapWith<SupplyManager>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public Guid StoreId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SupplyManager, SupplyManagerLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.FirstName, opt =>
                    opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt =>
                    opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.AvatarPhotoPath, opt =>
                    opt.MapFrom(x => x.User.AvatarPhotoPath))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath))
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(x => x.StoreId));
        }
    }
}