using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class OrderListVm
    {
        public IList<OrderLookupDto> Orders { get; set; }
    }
}
