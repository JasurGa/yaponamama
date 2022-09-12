using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList
{
    public class FindHeadRecruitersPagedListQuery : IRequest<PageDto<HeadRecruiterLookupDto>>
    {
        public string SearchQuery { get;set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

