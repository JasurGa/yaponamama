using Atlas.Application.CQRS.Providers.Commands.CreateProvider;
using Atlas.Application.CQRS.Providers.Commands.DeleteProvider;
using Atlas.Application.CQRS.Providers.Commands.UpdateProvider;
using Atlas.Application.CQRS.Providers.Queries.GetProviderDetails;
using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList;
using Atlas.Application.Models;
using Atlas.WebApi.Models;
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
    public class ProviderController : BaseController
    {
        private readonly IMapper _mapper;

        public ProviderController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get the paged list of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/provider/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto ProviderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ProviderLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetProviderPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/provider
        /// </remarks>
        /// <returns>Returns ProviderListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetProviderListQuery());

            return Ok(vm);
        }

        /// <summary>
        /// Get the provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Provider id (guid)</param>
        /// <returns>Returns ProviderDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderDetailsVm>> GetAsync(Guid id)
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
        /// POST /api/1.0/provider
        /// {
        ///     "name": "Sample name",
        ///     "description": "Sample description"
        ///     "address": "Sample address",
        ///     "latitude": 0,
        ///     "longitude": 0,
        ///     "LogotypePath": "/default/path"
        /// }
        /// </remarks>
        /// <param name="createProviderDto">CreateProviderDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
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
        /// PUT /api/1.0/provider
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     "name": "Sample name",
        ///     "description": "Sample description"
        ///     "address": "Sample address",
        ///     "latitude": 0,
        ///     "longitude": 0,
        ///     "LogotypePath": "/default/path"
        /// }
        /// </remarks>
        /// <param name="updateProviderDto">UpdateProviderDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateProviderDto updateProviderDto)
        {
            var command = _mapper.Map<UpdateProviderCommand>(updateProviderDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Provider id</param>
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
            await Mediator.Send(new DeleteProviderCommand 
            { 
                Id = id,
            });

            return NoContent();
        }
    }
}
