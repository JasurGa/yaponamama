using Atlas.Application.CQRS.Orders.Commands.CancelOrder;
using Atlas.Application.CQRS.Orders.Commands.DeleteOrder;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier;
using Atlas.Application.Models;
using AutoMapper;
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
        /// Get the order details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns OrderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetOrderDetailsQuery
            {
                Id       = id,
                ClientId = ClientId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the order details for courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/courier
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns OrderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}/courier")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderDetailsVm>> GetByForCourierAsync([FromRoute] Guid id) 
        {
            var vm = await Mediator.Send(new GetOrderDetailsForCourierQuery
            {
                Id        = id,
                CourierId = CourierId
            });

            return Ok(vm);
        }


        /// <summary>
        /// Get the list of last orders by client id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/order/last/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
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

        /// <summary>
        /// Get the list of last orders by courier id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/order/courier/last/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("courier/last/paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> GetLastOrdersByCourierIdAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByCourierQuery
            {
                CourierId = CourierId,
                PageIndex = pageIndex,
                PageSize  = pageSize
            }) ;

            return Ok(vm);
        }

        /// <summary>
        /// Deletes the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Order id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteOrderCommand
            {
                Id = id,
            });

            return Ok();
        }

        /// <summary>
        /// Cancel the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/cancel
        /// </remarks>
        /// <param name="id">Order id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}/cancel")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            await Mediator.Send(new CancelOrderCommand
            {
                Id = id,
            });

            return Ok();
        }
    }
}
