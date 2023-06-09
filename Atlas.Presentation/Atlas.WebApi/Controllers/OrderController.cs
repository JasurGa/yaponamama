﻿using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.CQRS.Orders.Commands.CancelOrderByClient;
using Atlas.Application.CQRS.Orders.Commands.ChangeOrderRefundStatus;
using Atlas.Application.CQRS.Orders.Commands.CreateOrder;
using Atlas.Application.CQRS.Orders.Commands.PayOrder;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrder;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderPrepayment;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderStatus;
using Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice;
using Atlas.Application.CQRS.Orders.Queries.FindOrderPagedList;
using Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByStore;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForAdmin;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier;
using Atlas.Application.Models;
using Atlas.SubscribeApi.Models;
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
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Calculate order price
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/order/calculateprice
        ///     {
        ///         "toLongitude": 0,
        ///         "toLatitude": 0,
        ///         "isPickup": true,
        ///         "promo": "string",
        ///         "goodToOrders": [
        ///             {
        ///                 "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "count": 10
        ///             }
        ///         ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="calculateOrderPriceDto">CalculateOrderPriceDto object</param>
        /// <returns>Returns PriceDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("calculateprice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PriceDetailsVm>> CalculatePriceAsync([FromBody] CalculateOrderPriceDto calculateOrderPriceDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CalculateOrderPriceDto,
                CalculateOrderPriceQuery>(calculateOrderPriceDto, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Search orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/order/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto OrderLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new FindOrderPagedListQuery
            {
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Pay the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/order/pay
        ///     {
        ///         "token": "asdasdasdsadsadsadsadsaasdsadsadasd",
        ///         "orderId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <param name="payOrderDto">PayOrderDto object</param>
        /// <returns>Returns SuccessDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("pay")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SuccessDetailsVm>> PayAsync([FromBody] PayOrderDto payOrderDto)
        {
            var vm = await Mediator.Send(_mapper.Map<PayOrderDto,
                PayOrderCommand>(payOrderDto, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Creates the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/order
        ///     {
        ///         "comment": "Test comment",
        ///         "dontCallWhenDelivered": false,
        ///         "apartment": 0,
        ///         "floor": 0,
        ///         "entrance": 0,
        ///         "toLongitude": 0,
        ///         "toLatitude": 0,
        ///         "isPickup": true,
        ///         "paymentType": 0,
        ///         "promo": "string",
        ///         "deliverAt": "2022-08-29T07:11:16.320Z",
        ///         "isDevVersionBot": "false",
        ///         "telegramUserId": 0123432432,
        ///         "goodReplacementType": 0,
        ///         "isPrivateHouse": false,
        ///         "goodToOrders": [
        ///             {
        ///                 "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "count": 10
        ///             }
        ///         ],
        ///     }
        ///     
        /// </remarks>
        /// <param name="createOrderDto">CreateOrderDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateOrderDto createOrderDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateOrderDto,
                CreateOrderCommand>(createOrderDto, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Change order refund status
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/refund/true
        ///     
        /// </remarks>
        /// <param name="id">Order id (Guid)</param>
        /// <param name="canBeRefund">Can be refund (bool)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.SupplyManager })]
        [HttpPatch("{id}/refund/{canBeRefund}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeRefundAsync([FromRoute] Guid id, [FromRoute] bool canBeRefund)
        {
            await Mediator.Send(new ChangeOrderRefundStatusCommand
            {
                OrderId     = id,
                CanBeRefund = canBeRefund
            });

            return NoContent();
        }

        /// <summary>
        /// Updates the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/order
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "courierId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "clientId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "comment": "Test comment",
        ///         "dontCallWhenDelivered": false,
        ///         "apartment": 0,
        ///         "floor": 0,
        ///         "entrance": 0,
        ///         "createdAt": "2022-01-01T10:00:00",
        ///         "deliverAt": "2022-01-01T10:00:00",
        ///         "finishedAt": "2022-01-01T10:00:00",
        ///         "purchasePrice": 100.0,
        ///         "sellingPrice": 100.0,
        ///         "status": 0,
        ///         "toLongitude": 10.00,
        ///         "toLatitude": -20.00,
        ///         "paymentType": 0,
        ///         "isPickup": false,
        ///         "promoId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "isDevVersionBot": "false",
        ///         "telegramUserId": 0123432432
        ///         "goodReplacementType": 0,
        ///         "isPrivateHouse": false
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateOrderDto">UpdateOrderDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateOrderDto updateOrderDto)
        {
            await Mediator.Send(_mapper.Map<UpdateOrderDto,
                UpdateOrderCommand>(updateOrderDto));

            return NoContent();
        }

        /// <summary>
        /// Updates the order's prepayment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/order/prepayment
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "isprepaid": true,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateOrderPrepayment">UpdateOrderPrepaymentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut("prepayment")]
        [AuthRoleFilter(new string[] { Roles.Support, Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdatePrepaymentAsync([FromBody] UpdateOrderPrepaymentDto updateOrderPrepayment)
        {
            await Mediator.Send(_mapper.Map<UpdateOrderPrepaymentDto,
                UpdateOrderPrepaymentCommand>(updateOrderPrepayment));

            return NoContent();
        }

        /// <summary>
        /// Updates the order status
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/order/status
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "status": 0,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateOrderDto">UpdateOrderStatusDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut("status")]
        [AuthRoleFilter(new string[] { Roles.Support, Roles.Admin, Roles.Courier, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateStatusAsync([FromBody] UpdateOrderStatusDto updateOrderDto)
        {
            await Mediator.Send(_mapper.Map<UpdateOrderStatusDto,
                UpdateOrderStatusCommand>(updateOrderDto));

            return NoContent();
        }

        /// <summary>
        /// Updates the order payment type
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/order/paymenttype
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "paymentType": 0,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateOrderPaymentTypeDto">UpdateOrderPaymentTypeDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut("paymenttype")]
        [AuthRoleFilter(new string[] { Roles.Support, Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdatePaymentTypeAsync([FromBody] UpdateOrderPaymentTypeDto updateOrderPaymentTypeDto)
        {
            await Mediator.Send(_mapper.Map<UpdateOrderPaymentTypeDto,
                UpdateOrderPaymentTypeCommand>(updateOrderPaymentTypeDto));

            return NoContent();
        }

        /// <summary>
        /// Get the order details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns OrderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.SupplyManager, Roles.Courier })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetOrderDetailsForAdminQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the order details for client
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/client/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Client id (guid)</param>
        /// <param name="showActive">Show active (bool)</param>
        /// <param name="pageSize">Page size (int)</param>
        /// <param name="pageIndex">Page index (int)</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("client/{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> GetAllForClientAsync([FromRoute] Guid id,
            [FromQuery] bool showActive = false,
            [FromQuery] int  pageSize   = 10,
            [FromQuery] int  pageIndex  = 0)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByClientQuery
            {
                ClientId   = id,
                ShowActive = showActive,
                PageSize   = pageSize,
                PageIndex  = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get orders for client
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/client
        ///     
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns ClientOrderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}/client")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClientOrderDetailsVm>> GetByIdForClientAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetOrderDetailsQuery
            {
                Id       = id,
                ClientId = ClientId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get orders for courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/courier/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Courier id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("courier/{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.Courier })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CourierOrderLookupDto>>> GetAllByForCourierAsync([FromRoute] Guid id,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByCourierQuery
            {
                PageSize  = pageSize,
                PageIndex = pageIndex,
                CourierId = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the order details for courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/courier
        ///     
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns OrderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}/courier")]
        [AuthRoleFilter(Roles.Courier)]
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
        /// Get orders for store
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> GetAllByForStoreAsync([FromRoute] Guid id,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByStoreQuery
            {
                StoreId   = id,
                PageSize  = pageSize,
                PageIndex = pageIndex,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/paged?pageIndex=0&amp;pageSize=10&amp;filterIsPrePayed=false&amp;filterPaymentType=0&amp;filterStatus=0&amp;filterFromCreatedAt=null&amp;filterToCreatedAt=null,
        ///     
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="filterIsPrePayed">Filter param for pre payed (bool)</param>
        /// <param name="filterPaymentType">Filter param for payment type (int)</param>
        /// <param name="filterStatus">Filter param for status (int)</param>
        /// <param name="filterFromCreatedAt">Filter for from created at (datetime)</param>
        /// <param name="filterToCreatedAt">Filter for to created at (datetime)</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Courier, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<OrderLookupDto>>> GetAllPagedAsync(
            [FromQuery] int       pageIndex           = 0, 
            [FromQuery] int       pageSize            = 10,
            [FromQuery] bool?     filterIsPrePayed    = null, 
            [FromQuery] int?      filterPaymentType   = null, 
            [FromQuery] int?      filterStatus        = null,
            [FromQuery] DateTime? filterFromCreatedAt = null,
            [FromQuery] DateTime? filterToCreatedAt   = null)
        {
            var vm = await Mediator.Send(new GetOrderPagedListQuery
            {
                PageIndex           = pageIndex,
                PageSize            = pageSize,
                FilterIsPrePayed    = filterIsPrePayed,
                FilterPaymentType   = filterPaymentType,
                FilterStatus        = filterStatus,
                FilterFromCreatedAt = filterFromCreatedAt,
                FilterToCreatedAt   = filterToCreatedAt
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of client's orders for bot
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/bot/paged?pageIndex=0&amp;pageSize=10&amp;status=1&amp;getCanceled=false
        ///     
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="status">Order status</param>
        /// <param name="getCanceled">Get canceled</param>
        /// <returns>Returns PageDto BotOrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("bot/paged")]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<BotOrderLookupDto>>> GetPagedBotOrdersAsync(
            [FromQuery] int  pageIndex   = 0,
            [FromQuery] int  pageSize    = 10,
            [FromQuery] int? status      = null,
            [FromQuery] bool getCanceled = false)
        {
            var vm = await Mediator.Send(new GetBotOrdersPagedListQuery
            {
                ClientId    = ClientId,
                Status      = status,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                GetCanceled = getCanceled
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of last orders for client
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/client/last/paged?pageIndex=0&amp;pageSize=10&amp;showActive=false
        ///     
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showActive">Show active only orders or not</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("client/last/paged")]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ClientOrderLookupDto>>> GetLastOrdersByClientIdAsync(
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 10,
            [FromQuery] bool showActive = false)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByClientQuery
            {
                ClientId   = ClientId,
                ShowActive = showActive,
                PageIndex  = pageIndex,
                PageSize   = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of last orders for courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/order/courier/last/paged?pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto OrderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("courier/last/paged")]
        [AuthRoleFilter(Roles.Courier)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CourierOrderLookupDto>>> GetLastOrdersByCourierIdAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastOrdersPagedListByCourierQuery
            {
                CourierId = CourierId,
                PageIndex = pageIndex,
                PageSize  = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Cancels the order (for client)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/order/a3eb7b4a-9f4e-4c71-8619-398655c563b8/cancel
        /// 
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}/cancel")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CancelOrderForClientAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new CancelOrderByClientCommand
            {
                Id       = id,
                ClientId = ClientId
            });

            return NoContent();
        }
    }
}