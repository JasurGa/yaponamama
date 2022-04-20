using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class OrderController : BaseController
    {
        /// <summary>
        /// Get the list of last orders by client id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/order/last/paged?pageIndex=0&pageSize=10
        /// </remarks>
        /// <param name="clientId">Client id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("last/paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> GetLastOrdersByClientIdAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByClientQuery
            {
                ClientId  = ClientId,
                PageIndex = pageIndex,
                PageSize  = pageSize
            });

            return Ok(vm);
        }
    }
}
