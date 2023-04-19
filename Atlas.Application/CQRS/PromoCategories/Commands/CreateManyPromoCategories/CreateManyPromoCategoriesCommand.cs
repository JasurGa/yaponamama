using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoCategories.Commands.CreateManyPromoCategories
{
    public class CreateManyPromoCategoriesCommand : IRequest<List<Guid>>
    {
        public List<CreatePromoCategoryLookupDto> PromoCategories { get; set; }
    }
}
