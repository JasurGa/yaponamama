using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Commands.UpdateUserLogin;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateUserLoginDto : IMapWith<UpdateUserLoginCommand>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserLoginDto, UpdateUserLoginCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Login, opt =>
                    opt.MapFrom(x => x.Login));
        }
    }
}
