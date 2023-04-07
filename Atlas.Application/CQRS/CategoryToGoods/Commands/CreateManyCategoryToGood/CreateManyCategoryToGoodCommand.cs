using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateManyCategoryToGood
{
    public class CreateManyCategoryToGoodCommand : IRequest
    {
        public List<CategoryToGoodLookupDto> CategoriesToGoods { get; set; }
    }
}
