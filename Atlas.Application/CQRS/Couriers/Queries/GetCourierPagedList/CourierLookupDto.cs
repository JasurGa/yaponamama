using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList
{
    public class CourierLookupDto : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public Guid VehicleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, CourierLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.FirstName, opt =>
                    opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt =>
                    opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Balance, opt =>
                    opt.MapFrom(x => x.Balance))
                .ForMember(x => x.KPI, opt =>
                    opt.MapFrom(x => x.KPI))
                .ForMember(x => x.VehicleId, opt =>
                    opt.MapFrom(x => x.VehicleId));
        }
    }
}