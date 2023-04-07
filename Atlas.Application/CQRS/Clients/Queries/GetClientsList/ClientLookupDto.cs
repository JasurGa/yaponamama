using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientsList
{
    public class ClientLookupDto : IMapWith<Client>
    {
        public Guid Id { get; set; }

        public UserLookupDto User { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPassportVerified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.IsPassportVerified, opt =>
                    opt.MapFrom(x => x.IsPassportVerified));
        }
    }
}
