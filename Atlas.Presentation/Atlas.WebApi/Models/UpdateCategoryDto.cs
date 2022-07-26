﻿using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Categories.Commands.UpdateCategory;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x => x.ImageUrl))
                .ForMember(x => x.IsMainCategory, opt =>
                    opt.MapFrom(x => x.IsMainCategory));
        }
    }
}
