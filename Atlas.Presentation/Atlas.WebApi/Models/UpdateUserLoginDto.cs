using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Commands.UpdateUserLogin;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateUserLoginDto : IMapWith<UpdateUserLoginCommand>
    {
        public string Login { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserLoginDto, UpdateUserLoginCommand>()
                .ForMember(x => x.Login, opt =>
                    opt.MapFrom(x => x.Login));
        }
    }
}
