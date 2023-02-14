using System;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Address { get; set; }

        public string AddressRu { get; set; }

        public string AddressEn { get; set; }

        public string AddressUz { get; set; }

        public string PhoneNumber { get; set; }

        public TimeSpan WorkStartsAt { get; set; }

        public TimeSpan WorkFinishesAt { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
