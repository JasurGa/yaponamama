using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Statistics.Queries.GetGoodsCountStatistics
{
    public class GoodsCountStatisticsVm
    {
        public List<GoodsCountLookupDto> Categories { get; set; }
    }
}
