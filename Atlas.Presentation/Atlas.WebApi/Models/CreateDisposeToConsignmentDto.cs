using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateDisposeToConsignmentDto : IMapWith<DisposeToConsignment>
    {
        public Guid ConsignmentId { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }
    }
}
