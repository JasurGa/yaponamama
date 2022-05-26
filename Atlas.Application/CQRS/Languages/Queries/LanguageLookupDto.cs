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
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
        }
    }
}
