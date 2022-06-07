using System;
namespace Atlas.Domain
{
    public class PageVisit
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public int VisitedCount { get; set; }
    }
}
