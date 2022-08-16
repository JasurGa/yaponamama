using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.RecommendationTypes.Queries.GetRecommendationTypesList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class RecommendationTypeController : BaseController
    {
        /// <summary>
        /// Gets recommendation type list
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/recommendationtype
        ///     
        /// </remarks>
        /// <returns>Returns RecommendationTypeListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RecommendationTypeListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetRecommendationTypesListQuery
            {
            });

            return Ok(vm);
        }
    }
}
