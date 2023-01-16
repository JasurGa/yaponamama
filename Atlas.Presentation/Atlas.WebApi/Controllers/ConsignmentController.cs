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
using Atlas.WebApi.Filters;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList;

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
        /// Search consignments
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/consignment/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="filterStartDate">Starting date</param>
        /// <param name="filterEndDate">Ending date</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order type: Ascending (true) || Descending (false)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto ConsignmentLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<ConsignmentLookupDto>>> SearchAsync(
            [FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] DateTime? filterStartDate = null, 
            [FromQuery] DateTime? filterEndDate = null,
            [FromQuery] string sortable = "Id",
            [FromQuery] bool ascending = true)
        {
            var vm = await Mediator.Send(new FindConsignmentPagedListQuery
            {
                SearchQuery     = searchQuery,
                PageSize        = pageSize,
                PageIndex       = pageIndex,
                FilterStartDate = filterStartDate,
                FilterEndDate   = filterEndDate,
                Sortable        = sortable,
                Ascending       = ascending
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of consignments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/consigment
        /// 
        /// </remarks>
        /// <returns>Returns ConsignmentListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
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
        /// 
        ///     GET /api/1.0/consigment/paged?pageIndex=0&amp;pageSize=10&amp;sortable=Name&amp;ascending=true
        /// 
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto ConsignmentLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ConsignmentLookupDto>>> GetAllPagedAsync(
            [FromQuery] int    pageIndex        = 0, 
            [FromQuery] int    pageSize         = 10,
            [FromQuery] string sortable         = "ShelfLocation",
            [FromQuery] bool   ascending        = true,
            [FromQuery] Guid?  filterCategoryId = null)
        {
            var vm = await Mediator.Send(new GetConsignmentPagedListQuery
            {
                PageIndex        = pageIndex,
                PageSize         = pageSize,
                Sortable         = sortable,
                Ascending        = ascending,
                FilterCategoryId = filterCategoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the specific consignment details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/consignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <param name="id">Consignment id (guid)</param>
        /// <returns>Returns ConsignmentDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
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
        ///     
        ///     POST /api/1.0/consignment
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "purchasedAt": "2022-05-14T14:12:02.953Z",
        ///         "expirateAt": "2022-05-14T14:12:02.953Z",
        ///         "shelfLocation": "1st shelf, 2nd box",
        ///         "count": 10
        ///     }
        ///     
        /// </remarks>
        /// <param name="createConsignment">CreateConsignmentDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateConsignmentDto createConsignment)
        {
            var consignmentId = await Mediator.Send(_mapper.Map<CreateConsignmentDto,
                CreateConsignmentCommand>(createConsignment));

            return Ok(consignmentId);
        }

        /// <summary>
        /// Updates the consignment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/consignment
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "purchasedAt": "2022-05-14T14:12:02.953Z",
        ///         "expirateAt": "2022-05-14T14:12:02.953Z",
        ///         "shelfLocation": "1st shelf, 2nd box"
        ///         "count": 10
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateConsignment">UpdateConsignmentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
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
        /// 
        ///     DELETE /api/1.0/consignment/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <param name="id">Consignment id</param>
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
