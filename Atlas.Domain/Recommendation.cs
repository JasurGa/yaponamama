using System;
namespace Atlas.Domain
{
    public class Recommendation
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid GoodId { get; set; }

        public Guid RecommendationTypeId { get; set; }
    }
}
