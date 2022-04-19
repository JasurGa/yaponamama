using System;
namespace Atlas.Domain
{
    public class Recommendation
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string IconPath { get; set; }

        public string Description { get; set; }

        public Guid RecommendationTypeId { get; set; }
    }
}
