using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class GetBotOrdersPagedListQuery : IRequest<PageDto<BotOrderLookupDto>>
    {
        public Guid ClientId { get; set; }

        public int? Status { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        
        public bool GetCanceled { get; set; }
    }
}
