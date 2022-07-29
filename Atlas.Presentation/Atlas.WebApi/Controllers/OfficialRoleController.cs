using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.OfficialRoles.Queries.GetOfficialRolesListQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class OfficialRoleController : BaseController
    {
        /// <summary>
        /// Gets the list of official roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/officialrole
        /// </remarks>
        /// <returns>Returns OfficialRoleListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OfficialRolesListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetOfficialRolesListQuery());
            return Ok(vm);
        }
    }
}
