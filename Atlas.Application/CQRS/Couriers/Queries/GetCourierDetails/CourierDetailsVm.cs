using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class CourierDetailsVm : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public UserDetailsVm User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public int Rate { get; set; }

        public Guid? VehicleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, CourierDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.User,
                    opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath,
                    opt => opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.DriverLicensePath,
                    opt => opt.MapFrom(src => src.DriverLicensePath))
                .ForMember(x => x.Balance,
                    opt => opt.MapFrom(src => src.Balance))
                .ForMember(x => x.KPI,
                    opt => opt.MapFrom(src => src.KPI))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(src => src.Rate))
                .ForMember(x => x.VehicleId,
                    opt => opt.MapFrom(src => src.VehicleId));
        }
    }
}
