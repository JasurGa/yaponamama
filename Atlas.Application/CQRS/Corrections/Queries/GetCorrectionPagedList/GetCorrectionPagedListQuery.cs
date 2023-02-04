using System;
using Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionPagedList
{
    public class GetCorrectionPagedListQuery : IRequest<PageDto<CorrectionLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool ShowDeleted { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }

        public Guid? FilterCategoryId { get; set; }
    }
}
