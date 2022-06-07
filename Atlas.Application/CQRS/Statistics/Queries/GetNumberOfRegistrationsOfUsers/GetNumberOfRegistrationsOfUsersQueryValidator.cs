using FluentValidation;

namespace Atlas.Application.CQRS.Statistics.Queries.GetNumberOfRegistrationsOfUsers
{
    public class GetNumberOfRegistrationsOfUsersQueryValidator : AbstractValidator<GetNumberOfRegistrationsOfUsersQuery>
    {
        public GetNumberOfRegistrationsOfUsersQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty();
        }
    }
}
