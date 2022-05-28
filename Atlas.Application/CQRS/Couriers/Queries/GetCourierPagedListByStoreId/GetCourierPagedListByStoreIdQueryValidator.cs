using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedListByStoreId
{
    public class GetCourierPagedListByStoreIdQueryValidator : AbstractValidator<GetCourierPagedListByStoreIdQuery>
    {
        public GetCourierPagedListByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
