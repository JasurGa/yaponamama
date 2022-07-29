using System;
using MediatR;

namespace Atlas.Application.CQRS.OfficialRoles.Queries.GetOfficialRolesListQuery
{
    public class GetOfficialRolesListQuery : IRequest<OfficialRolesListVm>
    {
    }
}
