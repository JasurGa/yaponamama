using System;
using Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetAllPagedPromoAdvertises
{
    public class GetAllPagedPromoAdvertisesQuery : IRequest<PageDto<PromoAdvertiseLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

