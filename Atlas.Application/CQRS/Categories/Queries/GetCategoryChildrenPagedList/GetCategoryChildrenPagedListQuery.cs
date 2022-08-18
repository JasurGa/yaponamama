using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildrenPagedList
{
    public class GetCategoryChildrenPagedListQuery : IRequest<PageDto<CategoryLookupDto>>
    {
        public Guid Id { get; set; }

        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
