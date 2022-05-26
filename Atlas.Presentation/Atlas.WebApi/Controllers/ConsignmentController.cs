using System;
using AutoMapper;
using Atlas.WebApi.Models;
using System.Threading.Tasks;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Atlas.Application.CQRS.Consignments.Commands.CreateConsignment;
using Atlas.Application.CQRS.Consignments.Commands.DeleteConsignment;
using Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentPagedList;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class ConsignmentController : BaseController
    {
        private readonly IMapper _mapper;

        public ConsignmentController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of consignments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/consigment
        /// </remarks>
        /// <returns>Returns ConsignmentListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ConsignmentListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetConsignmentListQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of consignments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/consigment/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto ConsignmentLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ConsignmentLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetConsignmentPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize  = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the specific consignment details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/consignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Consignment id (guid)</param>
        /// <returns>Returns ConsignmentDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ConsignmentDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetConsignmentDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new consignment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/consignment
        /// {
        ///     "storeToGoodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "purchasedAt":   "2022-05-14T14:12:02.953Z",
        ///     "expirateAt":    "2022-05-14T14:12:02.953Z",
        ///     "shelfLocation": "1st shelf, 2nd box",
        /// }
        /// </remarks>
        /// <param name="createConsignment">CreateConsignmentDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateConsignmentDto createConsignment)
        {
            var consignmentId = await Mediator.Send(_mapper
                .Map<CreateConsignmentCommand>(createConsignment));

            return Ok(consignmentId);
        }

        /// <summary>
        /// Updates the consignment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/consignment
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     "storeToGoodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "purchasedAt": "2022-05-14T14:12:02.953Z",
        ///     "expirateAt": "2022-05-14T14:12:02.953Z",
        ///     "shelfLocation": "1st shelf, 2nd box",
        /// }
        /// </remarks>
        /// <param name="updateConsignment">UpdateConsignmentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateConsignmentDto updateConsignment)
        {
            await Mediator.Send(_mapper.Map<UpdateConsignmentDto, UpdateConsignmentCommand>
                (updateConsignment));

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific consignment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/consignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Consignment id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteConsignmentCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
