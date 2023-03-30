using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateCardInfoToClientDto : IMapWith<CreateCardInfoToClientCommand>
    {
        public string Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCardInfoToClientDto, CreateCardInfoToClientCommand>()
                .ForMember(dst => dst.Token, opt => opt.MapFrom(src =>
                    src.Token));
        }
    }
}
