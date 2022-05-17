using MediatR;
using System;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreDetails
{
    public class GetStoreDetailsQuery : IRequest<StoreDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
