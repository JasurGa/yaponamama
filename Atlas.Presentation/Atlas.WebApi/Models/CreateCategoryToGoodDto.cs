using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Models
{
    public class CreateCategoryToGoodDto : IMapWith<CreateCategoryToGoodCommand>
    {
        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryToGoodDto, CreateCategoryToGoodCommand>()
                .ForMember(p => p.GoodId, opt =>
                    opt.MapFrom(p => p.GoodId))
                .ForMember(p => p.CategoryId, opt =>
                    opt.MapFrom(p => p.CategoryId));
        }
    }
}
