using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCardInfoToClientDto : IMapWith<CreateCardInfoToClientCommand>
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public string Expire { get; set; }

        public string Token { get; set; }

        public bool Recurrent { get; set; }

        public bool Verify { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCardInfoToClientDto, CreateCardInfoToClientCommand>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src =>
                    src.Name))
                .ForMember(dst => dst.Number, opt => opt.MapFrom(src =>
                    src.Number))
                .ForMember(dst => dst.Expire, opt => opt.MapFrom(src =>
                    src.Expire))
                .ForMember(dst => dst.Token, opt => opt.MapFrom(src =>
                    src.Token))
                .ForMember(dst => dst.Recurrent, opt => opt.MapFrom(src =>
                    src.Recurrent))
                .ForMember(dst => dst.Verify, opt => opt.MapFrom(src =>
                    src.Verify));
        }
    }
}
