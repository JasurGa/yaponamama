using System;
namespace Atlas.Domain
{
    public class PromoCategoryToGood
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid PromoCategoryId { get; set; }
    }
}

