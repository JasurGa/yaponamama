using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId
{
    public class PhotoToGoodLookupDto : IMapWith<PhotoToGood>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string PhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhotoToGood, PhotoToGoodLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath));
        }
    }
}