using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Orders.Commands.CreateOrder;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Atlas.Application.CQRS.OrderComments.Commands.CreateOrderComment;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrder;
using Atlas.Application.CQRS.OrderComments.Commands.UpdateOrderComment;
using Atlas.Application.CQRS.Orders.Commands.CancelOrderByClient;
using Atlas.Application.CQRS.OrderComments.Commands.DeleteOrderComment;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class OrderCommentController : BaseController
    {
        private readonly IMapper _mapper;

        public OrderCommentController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of order comments by order id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/ordercomment/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="orderId">Order id (guid)</param>
        /// <returns>Returns OrderCommentListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderCommentListVm>> GetListByOrderIdAsync([FromRoute] Guid orderId)
        {
            var vm = await Mediator.Send(new GetOrderCommentListByOrderQuery
            {
                OrderId = orderId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates an order comment for the admins
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/ordercomment
        ///     {
        ///         "orderId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "text": "My comment"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createOrderComment">CreateOrderCommentDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.Courier })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrderCommentDto createOrderComment)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateOrderCommentDto,
                CreateOrderCommentCommand>(createOrderComment, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.UserId = UserId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Updates the order comment for admins
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/ordercomment
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "text": "My new comment"
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateOrderComment">UpdateOrderCommentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.Courier })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateOrderCommentDto updateOrderComment)
        {
            await Mediator.Send(_mapper.Map<UpdateOrderCommentDto,
                UpdateOrderCommentCommand>(updateOrderComment, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.UserId = UserId;
                    });
                }));

            return NoContent();
        }

        /// <summary>
        /// Deletes the order comment for admins
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/ordercomment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.Courier })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromQuery] Guid id)
        {
            await Mediator.Send(new DeleteOrderCommentCommand
            {
                Id = id,
                UserId = UserId
            });

            return NoContent();
        }
    }
}
