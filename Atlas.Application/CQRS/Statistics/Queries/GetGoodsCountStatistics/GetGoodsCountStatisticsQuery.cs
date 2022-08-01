using System;
using MediatR;

namespace Atlas.Application.CQRS.Statistics.Queries.GetGoodsCountStatistics
{
    public class GetGoodsCountStatisticsQuery :
        IRequest<GoodsCountStatisticsVm>
    {
    }
}
