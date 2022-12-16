using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchCategoriesByGoodNameQuery : IRequest<SearchedCategoryListVm>
    {
        public string SearchQuery { get; set; }
    }
}

