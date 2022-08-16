using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient;
using Atlas.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class RecommendationController : BaseController
    {
        /// <summary>
        /// Returns recommendations for client
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/recommendation
        ///     
        /// </remarks>
        /// <returns>Returns RecommendationListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RecommendationListVm>> GetForClientAsync()
        {
            var vm = await Mediator.Send(new GetRecommendationListByClientQuery
            {
                ClientId = ClientId
            });

            return Ok(vm);
        }
    }
}
