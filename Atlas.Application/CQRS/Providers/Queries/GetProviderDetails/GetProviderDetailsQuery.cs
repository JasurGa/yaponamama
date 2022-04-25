using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderDetails
{
    public class GetProviderDetailsQuery : IRequest<ProviderDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
