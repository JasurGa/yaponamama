using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateDisposeToConsignmentDto : IMapWith<CreateDisposeToConsignmentCommand>
    {
        public Guid ConsignmentId { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateDisposeToConsignmentDto, CreateDisposeToConsignmentCommand>()
                .ForMember(dst => dst.ConsignmentId, opt =>
                    opt.MapFrom(src => src.ConsignmentId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment));
        }
    }
}
