using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentDetails
{
    public class DisposeToConsignmentDetailsVm : IMapWith<DisposeToConsignment>
    {
        public Guid Id { get; set; }

        public Guid ConsignmentId { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DisposeToConsignment, DisposeToConsignmentDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ConsignmentId, opt =>
                    opt.MapFrom(src => src.ConsignmentId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt));
        }
    }
}
