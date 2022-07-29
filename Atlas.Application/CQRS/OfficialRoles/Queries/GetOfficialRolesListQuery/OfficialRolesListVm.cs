using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.OfficialRoles.Queries.GetOfficialRolesListQuery
{
    public class OfficialRolesListVm
    {
        public List<OfficialRoleLookupDto> OfficialRoles { get; set; }
    }
}
