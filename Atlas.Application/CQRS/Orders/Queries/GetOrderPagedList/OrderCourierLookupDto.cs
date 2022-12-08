using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderPagedList
{
    public class OrderCourierLookupDto : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public int OrderCode
        {
            get
            {
                return (int)(Convert.ToInt64(Id.ToString().Split("-")[0], 16) % 1000000);
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, OrderCourierLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FirstName, opt =>
                    opt.MapFrom(src => src.User.FirstName))
                .ForMember(dst => dst.LastName, opt =>
                    opt.MapFrom(src => src.User.LastName));
        }
    }
}
