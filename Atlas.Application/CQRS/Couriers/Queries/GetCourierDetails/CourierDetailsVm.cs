using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class CourierDetailsVm : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public Guid? VehicleId { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, CourierDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.FirstName,
                    opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(x => x.LastName,
                    opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(x => x.Birthday,
                    opt => opt.MapFrom(src => src.User.Birthday))
                .ForMember(x => x.AvatarPhotoPath,
                    opt => opt.MapFrom(src => src.User.AvatarPhotoPath))
                .ForMember(x => x.PassportPhotoPath,
                    opt => opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.DriverLicensePath,
                    opt => opt.MapFrom(src => src.DriverLicensePath))
                .ForMember(x => x.Balance,
                    opt => opt.MapFrom(src => src.Balance))
                .ForMember(x => x.KPI,
                    opt => opt.MapFrom(src => src.KPI))
                .ForMember(x => x.VehicleId,
                    opt => opt.MapFrom(src => src.VehicleId));
        }
    }
}
