using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList
{
    public class PaymentTypeLookupDto : IMapWith<PaymentType>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentType, PaymentTypeLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name));
        }
    }
}