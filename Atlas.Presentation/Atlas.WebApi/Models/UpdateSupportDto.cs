using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Supports.Commands.UpdateSupport;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateSupportDto : IMapWith<UpdateSupportCommand>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSupportDto, UpdateSupportCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt =>
                    opt.MapFrom(x => x.UserId))
                .ForMember(x => x.InternalPhoneNumber, opt =>
                    opt.MapFrom(x => x.InternalPhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath));
        }
    }
}
