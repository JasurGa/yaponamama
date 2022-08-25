using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrdersByOrder;
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
    public class GoodToOrderController : BaseController
    {
        /// <summary>
        /// Gets the list of goods of an order by order id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/goodtoorder/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns GoodToOrderListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("order/{orderId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Client })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GoodToOrderListVm>> GetAllByOrderId([FromRoute] Guid orderId)
        {
            var vm = await Mediator.Send(new GetGoodToOrderListByOrderQuery
            {
                OrderId  = orderId
            });

            return Ok(vm);
        }
    }
}