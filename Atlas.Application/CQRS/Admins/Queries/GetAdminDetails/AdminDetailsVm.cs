﻿using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminDetails
{
    public class AdminDetailsVm : IMapWith<Admin>
    {
        public Guid Id { get; set; }

        public UserDetailsVm User { get; set; }

        public long KPI { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public Guid OfficialRoleId { get; set; }

        public string OfficialRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Admin, AdminDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.User,
                    opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.KPI,
                    opt => opt.MapFrom(src => src.KPI))
                .ForMember(x => x.StartOfWorkingHours,
                    opt => opt.MapFrom(src => src.StartOfWorkingHours))
                .ForMember(x => x.WorkingDayDuration,
                    opt => opt.MapFrom(src => src.WorkingDayDuration))
                .ForMember(x => x.Salary,
                    opt => opt.MapFrom(src => src.Salary))
                .ForMember(x => x.OfficialRoleId,
                    opt => opt.MapFrom(src => src.OfficialRoleId))
                .ForMember(x => x.OfficialRole,
                    opt => opt.MapFrom(src => src.OfficialRole.Name));
        }
    }
}
