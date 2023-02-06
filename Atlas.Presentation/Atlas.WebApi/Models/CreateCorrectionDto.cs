using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Corrections.Commands.CreateCorrection;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateCorrectionDto : IMapWith<CreateCorrectionCommand>
    {
        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public string CauseBy { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCorrectionDto, CreateCorrectionCommand>()
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(x => x.StoreId))
                .ForMember(x => x.GoodId, opt =>
                    opt.MapFrom(x => x.GoodId))
                .ForMember(x => x.CauseBy, opt =>
                    opt.MapFrom(x => x.CauseBy))
                .ForMember(x => x.Count, opt =>
                    opt.MapFrom(x => x.Count));
        }
    }
}
