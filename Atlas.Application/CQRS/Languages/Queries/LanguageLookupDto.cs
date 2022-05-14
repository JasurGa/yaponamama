using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Languages.Queries
{
    public class LanguageLookupDto : IMapWith<Language>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Language, LanguageLookupDto>()
                .ForMember(eVm => eVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eVm => eVm.Name,
                    opt => opt.MapFrom(e => e.Name));
        }
    }
}
