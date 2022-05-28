using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Commands.UpdateClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateClientDto : IMapWith<UpdateClientCommand>
    {
        public string PassportPhotoPath { get; set; }
        
        public string SelfieWithPassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClientDto, UpdateClientCommand>()
                .ForMember(x => x.PassportPhotoPath, opt => 
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.SelfieWithPassportPhotoPath, opt =>
                    opt.MapFrom(src => src.SelfieWithPassportPhotoPath));
        }
    }
}
