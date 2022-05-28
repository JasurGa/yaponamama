using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedList
{
    public class GetVehiclePagedListQueryValidator : AbstractValidator<GetVehiclePagedListQuery>
    {
        public GetVehiclePagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
