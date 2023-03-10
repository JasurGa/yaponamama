﻿using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PushNotifications.Queries.GetUnreadPushNotifications;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchedCategoryLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public int Count { get; set; }

        public int OrderNumber { get; set; }

        public bool IsHidden { get; set; }

        public bool IsVerified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, SearchedCategoryLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dst => dst.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dst => dst.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz))
                .ForMember(dst => dst.OrderNumber, opt =>
                    opt.MapFrom(src => src.OrderNumber))
                .ForMember(dst => dst.IsHidden, opt =>
                    opt.MapFrom(src => src.IsHidden))
                .ForMember(dst => dst.IsVerified, opt =>
                    opt.MapFrom(src => src.IsVerified));
        }
    }
}

