using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientDetails
{
    public class CardInfoToClientDetailsVm : IMapWith<CardInfoToClient>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string CardNumber { get; set; }

        public string DateOfIssue { get; set; }

        public string Cvc { get; set; }

        public string Cvc2 { get; set; }

        public string CardHolder { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CardInfoToClient, CardInfoToClientDetailsVm>()
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.CardNumber, opt =>
                    opt.MapFrom(src => src.CardNumber))
                .ForMember(dst => dst.DateOfIssue, opt =>
                    opt.MapFrom(src => src.DateOfIssue))
                .ForMember(dst => dst.Cvc, opt =>
                    opt.MapFrom(src => src.Cvc))
                .ForMember(dst => dst.Cvc2, opt =>
                    opt.MapFrom(src => src.Cvc2))
                .ForMember(dst => dst.CardHolder, opt =>
                    opt.MapFrom(src => src.CardHolder));
        }
    }
}
