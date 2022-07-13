using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedListNotByStoreId
{
    public class GetCourierPagedListNotByStoreIdQueryValidator :
        AbstractValidator<GetCourierPagedListNotByStoreIdQuery>
    {
        public GetCourierPagedListNotByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
