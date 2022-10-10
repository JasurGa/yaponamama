using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList
{
    public class CourierLookupDto : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public UserLookupDto User { get; set; }

        public VehicleLookupDto Vehicle { get; set; }

        public string PhoneNumber { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public int Rate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, CourierLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Balance, opt =>
                    opt.MapFrom(x => x.Balance))
                .ForMember(x => x.KPI, opt =>
                    opt.MapFrom(x => x.KPI))
                .ForMember(x => x.Rate, opt =>
                    opt.MapFrom(x => x.Rate))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.Vehicle, opt =>
                    opt.MapFrom(x => x.Vehicle));
        }
    }
}