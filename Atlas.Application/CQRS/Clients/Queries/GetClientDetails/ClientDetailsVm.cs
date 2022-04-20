using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientDetails
{
    public class ClientDetailsVm : IMapWith<Client>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsPassportVerified { get; set; }

        public long Balance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDetailsVm>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.SelfieWithPassportPhotoPath, opt =>
                    opt.MapFrom(src => src.SelfieWithPassportPhotoPath))
                .ForMember(dest => dest.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(dest => dest.IsPassportVerified, opt =>
                    opt.MapFrom(src => src.IsPassportVerified))
                .ForMember(dest => dest.Balance, opt =>
                    opt.MapFrom(src => src.Balance));
        }
    }
}
