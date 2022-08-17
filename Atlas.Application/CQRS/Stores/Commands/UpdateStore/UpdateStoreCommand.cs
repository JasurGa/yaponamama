using MediatR;
using System;

namespace Atlas.Application.CQRS.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Address { get; set; }

        public string AddressRu { get; set; }

        public string AddressEn { get; set; }

        public string AddressUz { get; set; }

        public string PhoneNumber { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
