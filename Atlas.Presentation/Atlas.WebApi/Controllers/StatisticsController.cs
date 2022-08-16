using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Statistics.Queries.GetGoodsCountStatistics;
using Atlas.Application.CQRS.Statistics.Queries.GetNumberOfRegistrationsOfUsers;
using Atlas.Application.CQRS.Statistics.Queries.GetOverallBalanceOfClients;
using Atlas.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class StatisticsController : BaseController
    {
        /// <summary>
        /// Get category goods count
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/statistics/good/count
        ///     
        /// </remarks>
        /// <returns>Returns GoodsCountStatisticsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("good/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GoodsCountStatisticsVm>> GetGoodsCountAsync()
        {
            var vm = await Mediator.Send(new GetGoodsCountStatisticsQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get overall balance from all clients
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/statistics/client/balances
        ///     
        /// </remarks>
        /// <returns>Returns (long) balance</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("client/balances")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<long>> GetOverallClientBalance()
        {
            var vm = await Mediator.Send(new GetOverallBalanceOfClientsQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get amount of registrations by users in a period of time
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/statistics/user/registrations?startDate=1900-01-01T10:00:00&amp;endDate=1900-01-04T10:00:00
        ///     
        /// </remarks>
        /// <param name="startDate">Starting date of the period</param>
        /// <param name="endDate">Ending date of the period</param>
        /// <returns>Returns (long) amount of registration </returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("user/registrations")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<long>> GetAmountOfRegistrations([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var vm = await Mediator.Send(new GetNumberOfRegistrationsOfUsersQuery
            {
                StartDate = startDate,
                EndDate   = endDate,
            });

            return Ok(vm);
        }
    }
}
