using Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback;
using Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails;
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
    public class OrderFeedbackController : BaseController
    {
        private readonly IMapper _mapper;

        public OrderFeedbackController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the order feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/orderfeedback/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">OrderFeedback id (guid)</param>
        /// <returns>Returns OrderFeedbackDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderFeedbackDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetOrderFeedbackDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates an order feedback
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/orderfeedback
        /// {
        ///     "orderId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "rating": "Good",
        ///     "text": "Overaill, it was great!",
        /// }
        /// </remarks>
        /// <param name="createOrderFeedback">CreateOrderFeedbackDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody]
            CreateOrderFeedbackDto createOrderFeedback)
        {
            var orderFeedbackId = await Mediator.Send(_mapper
                .Map<CreateOrderFeedbackDto, CreateOrderFeedbackCommand>
                (createOrderFeedback));

            return Ok(orderFeedbackId);
        }
    }
}
