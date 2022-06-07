using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.PageVisits.Queries.GetPagesVisits
{
    public class GetPagesVisitsQuery : IRequest<IList<int>>
    {
        public IList<string> Pages { get; set; }
    }
}
