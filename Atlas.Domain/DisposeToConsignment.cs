using System;

namespace Atlas.Domain
{
    public class DisposeToConsignment
    {
        public Guid Id { get; set; }

        public Guid ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
