using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.DeletePromoAdvertise
{
    public class DeletePromoAdvertiseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

