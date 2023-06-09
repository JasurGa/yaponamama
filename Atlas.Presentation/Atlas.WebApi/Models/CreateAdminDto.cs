﻿using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Admins.Commands.CreateAdmin;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateAdminDto : IMapWith<CreateAdminCommand>
    {
        public string PhoneNumber { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public Guid OfficialRoleId { get; set; }

        public int Salary { get; set; }

        public CreateUserDto User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAdminDto, CreateAdminCommand>()
                .ForMember(dst => dst.User, opt =>
                    opt.MapFrom(src => src.User))
                .ForMember(dst => dst.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.StartOfWorkingHours, opt =>
                    opt.MapFrom(src => src.StartOfWorkingHours))
                .ForMember(dst => dst.WorkingDayDuration, opt =>
                    opt.MapFrom(src => src.WorkingDayDuration))
                .ForMember(dst => dst.Salary, opt =>
                    opt.MapFrom(src => src.Salary))
                .ForMember(dst => dst.OfficialRoleId, opt =>
                    opt.MapFrom(src => src.OfficialRoleId));
        }
    }
}
