using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Commands.UpdateClient;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateClientDto : IMapWith<UpdateClientCommand>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public bool IsPassportVerified { get; set; }

        public UpdateUserDto User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClientDto, UpdateClientCommand>()
                .ForMember(x => x.Id, opt => 
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.SelfieWithPassportPhotoPath, opt =>
                    opt.MapFrom(src => src.SelfieWithPassportPhotoPath))
                .ForMember(x => x.IsPassportVerified, opt =>
                    opt.MapFrom(src => src.IsPassportVerified))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(src => src.User));
        }
    }
}
