using MediatR;
using System;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportDetails
{
    public class GetSupportDetailsQuery : IRequest<SupportDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
