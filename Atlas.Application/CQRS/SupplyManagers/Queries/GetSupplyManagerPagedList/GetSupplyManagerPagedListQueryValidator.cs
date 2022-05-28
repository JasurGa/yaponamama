using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList
{
    public class GetSupplyManagerPagedListQueryValidator : AbstractValidator<GetSupplyManagerPagedListQuery>
    {
        public GetSupplyManagerPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
