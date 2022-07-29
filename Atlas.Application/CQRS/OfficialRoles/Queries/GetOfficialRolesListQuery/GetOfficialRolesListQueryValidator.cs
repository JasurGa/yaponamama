using System;
using FluentValidation;

namespace Atlas.Application.CQRS.OfficialRoles.Queries.GetOfficialRolesListQuery
{
    public class GetOfficialRolesListQueryValidator :
        AbstractValidator<GetOfficialRolesListQuery>
    {
        public GetOfficialRolesListQueryValidator()
        {
        }
    }
}
