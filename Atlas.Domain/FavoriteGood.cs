using System;
namespace Atlas.Domain
{
    public class FavoriteGood
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid GoodId { get; set; }

        public Good Good { get; set; }

        public DateTime CreatedAt { get; set;  }
    }
}
