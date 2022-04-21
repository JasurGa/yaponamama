﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class CourierDetailsVm : IMapWith<Courier>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Courier, CourierDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
