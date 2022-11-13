using System;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryPagedList
{
    public class GetPromoCategoryPagedListQuery : IRequest<PageDto<PromoCategoryLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool ShowDeleted { get; set; }
    }
}

