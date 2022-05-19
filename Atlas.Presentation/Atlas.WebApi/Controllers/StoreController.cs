using Atlas.Application.CQRS.Stores.Commands.CreateStore;
using Atlas.Application.CQRS.Stores.Commands.DeleteStore;
using Atlas.Application.CQRS.Stores.Commands.RestoreStore;
using Atlas.Application.CQRS.Stores.Commands.UpdateStore;
using Atlas.Application.CQRS.Stores.Queries.GetStoreDetails;
using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.CQRS.Stores.Queries.GetStorePagedList;
using Atlas.Application.Models;
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
    public class StoreController : BaseController
    {
        private readonly IMapper _mapper;

        public StoreController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of stores
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/store
        /// </remarks>
        /// <returns>Returns StoreListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetStoreListQuery());

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/store/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto ProviderLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<StoreLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetStorePagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the store details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <returns>Returns StoreDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetStoreDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Deletes the store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
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
            await Mediator.Send(new DeleteStoreCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Creates a new store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/store
        /// {
        ///     "name": "Sample name",
        ///     "address": "Sample address",
        ///     "longitude": 0,
        ///     "longitude": 0
        /// }
        /// </remarks>
        /// <param name="createStore">CreateStoreDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateStoreDto createStore)
        {
            var command = _mapper.Map<CreateStoreCommand>(createStore);

            var storeId = await Mediator.Send(command);

            return Ok(storeId);
        }

        /// <summary>
        /// Updates the store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/store
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "name": "Sample name",
        ///     "address": "Sample address",
        ///     "longitude": 0,
        ///     "longitude": 0,
        ///     "isDeleted": false,
        /// }
        /// </remarks>
        /// <param name="updateStore">UpdateStoreDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateStoreDto updateStore)
        {
            var command = _mapper.Map<UpdateStoreCommand>(updateStore);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Restore the specific store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync(Guid id)
        {
            await Mediator.Send(new RestoreStoreCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
