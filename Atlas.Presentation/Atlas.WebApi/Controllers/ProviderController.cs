using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Providers.Commands.CreateProvider;
using Atlas.Application.CQRS.Providers.Commands.DeleteProvider;
using Atlas.Application.CQRS.Providers.Commands.RestoreProvider;
using Atlas.Application.CQRS.Providers.Commands.UpdateProvider;
using Atlas.Application.CQRS.Providers.Queries.FindProviderPagedList;
using Atlas.Application.CQRS.Providers.Queries.GetProviderDetails;
using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList;
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
    public class ProviderController : BaseController
    {
        private readonly IMapper _mapper;

        public ProviderController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search providers
        /// </summary>
        /// <remarks>
        ///
        ///     GET /api/1.0/provider/search?searchQuery=bla+bla+bla&amp;pageSize=0&amp;pageIndex=0&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <param name="showDeleted">Show deleted (bool)</param>
        /// <returns>PageDto ProviderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<ProviderLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new FindProviderPagedListQuery
            {
                SearchQuery = searchQuery,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/provider/paged?pageIndex=0&amp;pageSize=10&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="search">Search string</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto ProviderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ProviderLookupDto>>> GetAllPagedAsync(
            [FromQuery] string search = "", 
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortable = "Name",
            [FromQuery] bool ascending = true,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetProviderPagedListQuery
            {
                Search      = search,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                Sortable    = sortable,
                Ascending   = ascending,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/provider
        ///     
        /// </remarks>
        /// <param name="search">Search string</param>
        /// <returns>Returns ProviderListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderListVm>> GetAllAsync([FromQuery] string search = "",
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetProviderListQuery 
            {
                Search      = search,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Provider id (guid)</param>
        /// <returns>Returns ProviderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderDetailsVm>> GetAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetProviderDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new provider
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/provider
        ///     {
        ///         "name": "Sample name",
        ///         "description": "Sample description"
        ///         "address": "Sample address",
        ///         "latitude": 0,
        ///         "longitude": 0,
        ///         "logotypePath": "/default/path"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createProviderDto">CreateProviderDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateProviderDto createProviderDto)
        {
            var command = _mapper.Map<CreateProviderCommand>(createProviderDto);

            var providerId = await Mediator.Send(command);

            return Ok(providerId);
        }

        /// <summary>
        /// Updates the provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/provider
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         "name": "Sample name",
        ///         "description": "Sample description"
        ///         "address": "Sample address",
        ///         "latitude": 0,
        ///         "longitude": 0,
        ///         "logotypePath": "/default/path"
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateProviderDto">UpdateProviderDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateProviderDto updateProviderDto)
        {
            await Mediator.Send(_mapper.Map<UpdateProviderDto,
                UpdateProviderCommand>(updateProviderDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Provider id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteProviderCommand 
            { 
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restores a specific provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH /api/1.0/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Provider id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreProviderCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
