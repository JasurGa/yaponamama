using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Commands.RestorePromoCategory
{
    public class RestorePromoCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

