﻿using System;
using AutoMapper;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;

namespace Atlas.WebApi.Models
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int Sex { get; set; }

        public DateTime Birthday { get; set; }
        
        public string AvatarPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.Login, opt =>
                    opt.MapFrom(src => src.Login))
                .ForMember(x => x.Password, opt =>
                    opt.MapFrom(src => src.Password))
                .ForMember(x => x.FirstName, opt => 
                    opt.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, opt => 
                    opt.MapFrom(src => src.LastName))
                .ForMember(x => x.MiddleName, opt =>
                    opt.MapFrom(src => src.MiddleName))
                .ForMember(x => x.Sex, opt =>
                    opt.MapFrom(src => src.Sex))
                .ForMember(x => x.Birthday, opt => 
                    opt.MapFrom(src => src.Birthday))
                .ForMember(x => x.AvatarPhotoPath, opt =>
                    opt.MapFrom(src => src.AvatarPhotoPath));
        }
    }
}
