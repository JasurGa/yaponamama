using MediatR;
using System;

namespace Atlas.Application.CQRS.Providers.Commands.UpdateProvider
{
    public class UpdateProviderCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string LogotypePath { get; set; }
    }
}
