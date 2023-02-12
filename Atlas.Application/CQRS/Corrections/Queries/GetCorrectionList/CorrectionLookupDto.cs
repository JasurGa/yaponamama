using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList
{
    public class CorrectionLookupDto : IMapWith<Correction>
    {
        public Guid Id { get; set; }

        public StoreLookupDto Store { get; set; }

        public GoodLookupDto Good { get; set; }

        public UserLookupDto User { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CauseBy { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Correction, CorrectionLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Store, opt =>
                    opt.MapFrom(x => x.StoreToGood.Store))
                .ForMember(x => x.Good, opt =>
                    opt.MapFrom(x => x.StoreToGood.Good))
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.CreatedAt, opt =>
                    opt.MapFrom(x => x.CreatedAt))
                .ForMember(x => x.CauseBy, opt =>
                    opt.MapFrom(x => x.CauseBy))
                .ForMember(x => x.Count, opt =>
                    opt.MapFrom(x => x.Count));
        }
    }
}
