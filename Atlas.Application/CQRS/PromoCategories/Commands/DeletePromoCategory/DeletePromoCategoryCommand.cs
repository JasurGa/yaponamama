using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Commands.DeletePromoCategory
{
    public class DeletePromoCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

