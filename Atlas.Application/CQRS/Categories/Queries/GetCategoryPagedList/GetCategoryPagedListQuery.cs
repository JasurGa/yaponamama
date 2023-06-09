﻿using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQuery : IRequest<PageDto<CategoryLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public bool ShowHidden { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }
    }
}
