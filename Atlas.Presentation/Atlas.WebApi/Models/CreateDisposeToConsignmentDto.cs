using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateDisposeToConsignmentDto : IMapWith<CreateDisposeToConsignmentCommand>
    {
        public Guid ConsignmentId { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }
    }
}
