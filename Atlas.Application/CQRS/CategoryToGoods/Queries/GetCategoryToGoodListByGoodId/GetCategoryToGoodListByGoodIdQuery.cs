using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQuery : IRequest<CategoryToGoodListVm>
    {
        public Guid GoodId { get; set; }
    }
}
