using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Commands.VerifyClient;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class VerifyClientDto : IMapWith<VerifyClientCommand>
    {
        public Guid ClientId { get; set; }

        public bool IsPassportVerified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerifyClientDto, VerifyClientCommand>()
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.IsPassportVerified, opt =>
                    opt.MapFrom(src => src.IsPassportVerified));
        }
    }
}
