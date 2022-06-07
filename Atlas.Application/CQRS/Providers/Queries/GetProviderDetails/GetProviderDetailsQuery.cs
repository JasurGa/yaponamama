using MediatR;
using System;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderDetails
{
    public class GetProviderDetailsQuery : IRequest<ProviderDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
