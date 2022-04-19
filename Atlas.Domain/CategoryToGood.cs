using System;
namespace Atlas.Domain
{
    public class CategoryToGood
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
