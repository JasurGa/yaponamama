using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Admins.Commands.UpdateAdmin;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateAdminDto : IMapWith<UpdateAdminCommand>
    {
        public Guid Id { get; set; }

        public long KPI { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public Guid OfficialRoleId { get; set; }

        public UpdateUserDto User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAdminDto, UpdateAdminCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.KPI, opt =>
                    opt.MapFrom(src => src.KPI))
                .ForMember(dst => dst.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.StartOfWorkingHours, opt =>
                    opt.MapFrom(src => src.StartOfWorkingHours))
                .ForMember(dst => dst.WorkingDayDuration, opt =>
                    opt.MapFrom(src => src.WorkingDayDuration))
                .ForMember(dst => dst.OfficialRoleId, opt =>
                    opt.MapFrom(src => src.OfficialRoleId))
                .ForMember(dst => dst.User, opt =>
                    opt.MapFrom(src => src.User));
        }

    }
}
