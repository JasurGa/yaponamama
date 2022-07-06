using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminDetails
{
    public class GetAdminDetailsQueryValidator :
        AbstractValidator<GetAdminDetailsQuery>
    {
        public GetAdminDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
