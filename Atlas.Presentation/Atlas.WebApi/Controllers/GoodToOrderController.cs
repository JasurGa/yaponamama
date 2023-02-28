using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.GoodToOrders.Commands.DeleteGoodToOrder;
using Atlas.Application.CQRS.GoodToOrders.Commands.RecreateGoodToOrders;
using Atlas.Application.CQRS.GoodToOrders.Commands.UpdateGoodToOrder;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderPagedList;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public GoodToOrderController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the paged list of sold goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/goodtoorder/paged
        ///     
        /// </remarks>
        /// <param name="pageSize">Page size (int)</param>
        /// <param name="pageIndex">Page index (int)</param>
        /// <returns>Returns PageDto GoodToOrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<GoodToOrderLookupDto>>> GetAllPaged(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetGoodToOrderPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

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
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.Client })]
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

        /// <summary>
        /// Rewrites the list of goods in a specific order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/goodtoorder/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     {
        ///         goodToOrders: [
        ///             {
        ///                 goodId: a3eb7b4a-9f4e-4c71-8619-398655c563b8,
        ///                 count: 10
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <param name="orderId">Order id (guid)</param>
        /// <param name="recreateGoodToOrders">RecreateGoodToOrdersDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("{orderId}")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromRoute] Guid orderId, [FromBody] RecreateGoodToOrdersDto recreateGoodToOrders)
        {
            var command = _mapper.Map<RecreateGoodToOrdersDto, RecreateGoodToOrdersCommand>(recreateGoodToOrders, opt => 
                opt.AfterMap((src, dst) => dst.OrderId = orderId));

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        /// <summary>
        /// Updates the good in an order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/goodtoorder
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "count": 10
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateGoodToOrder">UpdateGoodToOrderDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdateGoodToOrderDto updateGoodToOrder)
        {
            await Mediator.Send(_mapper.Map<UpdateGoodToOrderDto,
                UpdateGoodToOrderCommand>(updateGoodToOrder));

            return NoContent();
        }

        /// <summary>
        /// Deletes the good in an order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/goodtoorder/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">GoodToOrder id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteGoodToOrderCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}