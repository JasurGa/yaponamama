using FluentValidation;

namespace Atlas.Application.CQRS.Statistics.Queries.GetNumberOfRegistrationsOfUsers
{
    public class GetNumberOfRegistrationsOfUsersQueryValidator : AbstractValidator<GetNumberOfRegistrationsOfUsersQuery>
    {
        public GetNumberOfRegistrationsOfUsersQueryValidator()
        {
            RuleFor(nor => nor.StartDate)
                .NotEmpty();

            RuleFor(nor => nor.EndDate)
                .NotEmpty();
        }
    }
}
