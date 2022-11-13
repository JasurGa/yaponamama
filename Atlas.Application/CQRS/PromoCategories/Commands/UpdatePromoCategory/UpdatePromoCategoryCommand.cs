using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Commands.UpdatePromoCategory
{
    public class UpdatePromoCategoryCommand : IRequest
    {
        public Guid Id { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }
    }
}

