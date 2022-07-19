using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateSupplyManagerDto : IMapWith<UpdateSupplyManagerCommand>
    {
        public Guid Id { get; set; }

        public UpdateUserDto User { get; set; }

        public Guid StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSupplyManagerDto, UpdateSupplyManagerCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(src => src.User))
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(src => src.StoreId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.StartOfWorkingHours, opt =>
                    opt.MapFrom(src => src.StartOfWorkingHours))
                .ForMember(x => x.WorkingDayDuration, opt =>
                    opt.MapFrom(src => src.WorkingDayDuration))
                .ForMember(x => x.Salary, opt =>
                    opt.MapFrom(src => src.Salary));
        }
    }
}
