using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportDetails
{
    public class SupportDetailsVm : IMapWith<Support>
    {
        public Guid Id { get; set; }

        public UserDetailsVm User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public long KPI { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Support, SupportDetailsVm>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.FirstName, opt =>
                    opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt =>
                    opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.AvatarPhotoPath, opt =>
                    opt.MapFrom(x => x.User.AvatarPhotoPath))
                .ForMember(x => x.StartOfWorkingHours, opt =>
                    opt.MapFrom(x => x.StartOfWorkingHours))
                .ForMember(x => x.WorkingDayDuration, opt =>
                    opt.MapFrom(x => x.WorkingDayDuration))
                .ForMember(x => x.Salary, opt =>
                    opt.MapFrom(x => x.Salary))
                .ForMember(x => x.KPI, opt =>
                    opt.MapFrom(x => x.KPI))
                .ForMember(x => x.InternalPhoneNumber, opt =>
                    opt.MapFrom(x => x.InternalPhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath))
                .ForMember(x => x.IsDeleted, opt =>
                    opt.MapFrom(x => x.IsDeleted));
        }
    }
}
