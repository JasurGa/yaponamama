using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetAvailableGoodList
{
    public class GetAvailableGoodListQuery : IRequest<List<Guid>>
    {

    }
}
