using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.DeletePromoAdvertisePage
{
    public class DeletePromoAdvertisePageCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

