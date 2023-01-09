using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart
{
	public class LackingGoodListVm
	{
		public ICollection<LackingGoodLookupDto> LackingGoods { get; set; }
	}
}

