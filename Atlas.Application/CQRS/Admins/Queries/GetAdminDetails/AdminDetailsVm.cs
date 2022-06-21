using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminDetails
{
    public class AdminDetailsVm : IMapWith<Admin>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public long KPI { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public Guid OfficialRoleId { get; set; }

        public string OfficialRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Admin, AdminDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.FirstName,
                    opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(x => x.LastName,
                    opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(x => x.AvatarPhotoPath,
                    opt => opt.MapFrom(src => src.User.AvatarPhotoPath))
                .ForMember(x => x.KPI,
                    opt => opt.MapFrom(src => src.KPI))
                .ForMember(x => x.StartOfWorkingHours,
                    opt => opt.MapFrom(src => src.StartOfWorkingHours))
                .ForMember(x => x.WorkingDayDuration,
                    opt => opt.MapFrom(src => src.WorkingDayDuration))
                .ForMember(x => x.OfficialRoleId,
                    opt => opt.MapFrom(src => src.OfficialRoleId))
                .ForMember(x => x.OfficialRole,
                    opt => opt.MapFrom(src => src.OfficialRole.Name));
        }
    }
}
