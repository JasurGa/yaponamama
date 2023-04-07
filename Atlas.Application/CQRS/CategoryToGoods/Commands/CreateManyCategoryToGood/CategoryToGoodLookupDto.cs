using System;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateManyCategoryToGood
{
    public class CategoryToGoodLookupDto
    {
        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }
    }
}