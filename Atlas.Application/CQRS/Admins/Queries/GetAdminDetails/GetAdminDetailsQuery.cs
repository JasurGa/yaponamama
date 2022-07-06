using System;
using MediatR;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminDetails
{
    public class GetAdminDetailsQuery : IRequest<AdminDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
