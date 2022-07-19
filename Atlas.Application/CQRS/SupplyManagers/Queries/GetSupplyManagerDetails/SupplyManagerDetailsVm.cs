using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerDetails
{
    public class SupplyManagerDetailsVm : IMapWith<SupplyManager>
    {
        public Guid Id { get; set; }

        public UserDetailsVm User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public int Salary { get; set; }

        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SupplyManager, SupplyManagerDetailsVm>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath))
                .ForMember(x => x.Salary, opt =>
                    opt.MapFrom(x => x.Salary))
                .ForMember(x => x.IsDeleted, opt =>
                    opt.MapFrom(x => x.IsDeleted));
        }
    }
}