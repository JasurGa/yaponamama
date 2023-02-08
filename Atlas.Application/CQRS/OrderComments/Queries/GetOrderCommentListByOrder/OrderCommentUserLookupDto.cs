using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class OrderCommentUserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, OrderCommentUserLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.LastName));
        }
    }
}
