using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateSupplyManagerDto : IMapWith<CreateSupplyManagerCommand>
    {
        public CreateUserDto User { get; set; }

        public Guid StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSupplyManagerDto, CreateSupplyManagerCommand>()
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(x => x.StoreId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath))
                .ForMember(x => x.StartOfWorkingHours, opt =>
                    opt.MapFrom(x => x.StartOfWorkingHours))
                .ForMember(x => x.WorkingDayDuration, opt =>
                    opt.MapFrom(x => x.WorkingDayDuration))
                .ForMember(x => x.Salary, opt =>
                    opt.MapFrom(x => x.Salary));
        }
    }
}
