using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class CourierOrderClientDetailsVm : IMapWith<Client>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, CourierOrderClientDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FirstName, opt =>
                    opt.MapFrom(src => src.User.FirstName))
                .ForMember(dst => dst.LastName, opt =>
                    opt.MapFrom(src => src.User.LastName))
                .ForMember(dst => dst.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
