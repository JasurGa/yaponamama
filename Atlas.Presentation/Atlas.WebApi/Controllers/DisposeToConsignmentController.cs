using Atlas.Application.Common.Constants;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment;
using Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentDetails;
using Atlas.Application.CQRS.DisposeToConsignments.Commands.DeleteDisposeToConsignment;
using Atlas.Application.Models;
using Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList;
using Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.CQRS.DisposeToConsignments.Queries.FindDisposeToConsignmentPagedList;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class DisposeToConsignmentController : BaseController
    {
        private readonly IMapper _mapper;

        public DisposeToConsignmentController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a consignment dispose record
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/disposetoconsignment
        ///     {
        ///         "consignmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "count": 12
        ///         "comment": "Hey, this is a simple comment for the consignment dispose"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createDisposeToConsignmentDto">CreateDisposeToConsignmentDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateDisposeToConsignmentDto createDisposeToConsignmentDto)
        {
            var command = _mapper.Map<CreateDisposeToConsignmentCommand>(createDisposeToConsignmentDto);

            var disposeToConsignmentId = await Mediator.Send(command);

            return Ok(disposeToConsignmentId);
        }

        /// <summary>
        /// Deletes a specific consignment dispose record by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/disposetoconsignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">DisposeToConsignment id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteDisposeToConsignmentCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Get the consignment dispose record by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/disposetoconsignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">DisposeToConsignment id (guid)</param>
        /// <returns>Returns DisposeToConsignmentDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DisposeToConsignmentDetailsVm>> GetAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetDisposeToConsignmentDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Search for consignment dispose records
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/disposetoconsignment/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto ConsignmentLookupDto</returns>
        /// <response code="200">Success</response>c
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<DisposeToConsignmentLookupDto>>> SearchAsync(
            [FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize  = 10)
        {
            var vm = await Mediator.Send(new FindDisposeToConsignmentPagedListQuery
            {
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of consignment dispose records
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/disposetoconsignment/paged?pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto DisposeToConsignmentLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<DisposeToConsignmentLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetDisposeToConsignmentPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }
    }
}
