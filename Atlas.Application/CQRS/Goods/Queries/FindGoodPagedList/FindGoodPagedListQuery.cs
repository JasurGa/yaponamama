﻿using System;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.FindGoodPagedList
{
    public class FindGoodPagedListQuery : IRequest<PageDto<GoodLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public string SearchQuery { get; set; }

        public double MinSimilarity { get; set; }

        public Guid? FilterCategoryId { get; set; }

        public int? FilterMinSellingPrice { get; set; }

        public int? FilterMaxSellingPrice { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Guid ClientId { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}

