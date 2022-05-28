using System;
using MediatR;

namespace Atlas.Application.CQRS.Promos.Commands.DeletePromo
{
    public class DeletePromoCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
