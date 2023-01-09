using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart
{
	public class UpdateManyGoodsInCartCommand : IRequest<LackingGoodListVm>
	{
		public Guid ClientId { get; set; }

		public IDictionary<Guid, int> GoodsToCount { get; set; }
	}
}

