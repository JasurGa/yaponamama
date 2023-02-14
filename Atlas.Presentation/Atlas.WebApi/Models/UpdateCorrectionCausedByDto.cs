using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Corrections.Commands.UpdateCorrectionCausedBy;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateCorrectionCausedByDto : IMapWith<UpdateCorrectionCausedByCommand>
    {
        public Guid Id { get; set; }

        public string CausedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCorrectionCausedByDto, UpdateCorrectionCausedByCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.CausedBy, opt =>
                    opt.MapFrom(x => x.CausedBy));
        }
    }
}
