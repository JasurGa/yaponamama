using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Commands.CreatePromoCategory
{
    public class CreatePromoCategoryCommand : IRequest<Guid>
    {
        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }
    }
}

