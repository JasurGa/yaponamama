using System;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        
        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
