using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager;
using Atlas.Application.CQRS.SupplyManagers.Commands.DeleteSupplyManager;
using Atlas.Application.CQRS.SupplyManagers.Commands.RestoreSupplyManager;
using Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager;
using Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManagersStoreId;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerDetails;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedListByStoreId;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedListNotByStoreId;
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
    public class SupplyManagerController : BaseController
    {
        private readonly IMapper _mapper;

        public SupplyManagerController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the paged list of supply managers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/supplymanager/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto SupplyManagerLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<SupplyManagerLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetSupplyManagerPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of supply managers by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/supplymanager/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?&amp;showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto SupplyManagerLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{storeId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<SupplyManagerLookupDto>>> GetAllPagedAsync([FromRoute] Guid storeId, [FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetSupplyManagerPagedListByStoreIdQuery
            {
                StoreId     = storeId,
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of all supply managers except supply managers which contains store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/supplymanager/except/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?&amp;showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto SupplyManagerLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("except/store/{storeId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<SupplyManagerLookupDto>>> GetNotByStoreIdPagedAsync(
            [FromRoute] Guid storeId, [FromQuery] bool showDeleted = false,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetSupplyManagerPagedListNotByStoreIdQuery
            {
                StoreId     = storeId,
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the supply manager details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/supplymanager/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">SupplyManager id (guid)</param>
        /// <returns>Returns SupplyManagerDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SupplyManagerDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetSupplyManagerDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new supply manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/supplymanager
        /// {
        ///     "userId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "storeId": "j3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "phoneNumber": "+998901234567"
        ///     "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///     "salary": 1000000,
        ///     "startOfWorkingHours": "01-01-1901T10:00:00",
        ///     "workingDayDuration": 8,
        /// }
        /// </remarks>
        /// <param name="createSupplyManager">CreateSupplyManagerDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="401">Not found</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateSupplyManagerDto createSupplyManager)
        {
            var supplyManagerId = await Mediator.Send(_mapper.Map<CreateSupplyManagerDto,
                CreateSupplyManagerCommand>(createSupplyManager));

            return Ok(supplyManagerId);
        }

        /// <summary>
        /// Adds supply managers to store 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/supplymanager/store
        /// {
        ///     "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "supplyManagerIds": [
        ///         "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     ]
        /// }
        /// </remarks>
        /// <param name="updateSupplyManagersStoreIdDto">UpdateSupplyManagersStoreIdDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut("store")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateStoreIdAsync([FromBody] UpdateSupplyManagersStoreIdDto updateSupplyManagersStoreIdDto)
        {
            await Mediator.Send(_mapper.Map<UpdateSupplyManagersStoreIdDto,
                UpdateSupplyManagersStoreIdCommand>(updateSupplyManagersStoreIdDto));
            return NoContent();
        }

        /// <summary>
        /// Updates the supply manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/supplymanager
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "userId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "storeId": "j3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "phoneNumber": "+998901234567"
        ///     "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///     "salary": 1000000,
        ///     "startOfWorkingHours": "01-01-1901T10:00:00",
        ///     "workingDayDuration": 8,
        /// }
        /// </remarks>
        /// <param name="updateSupplyManager">UpdateSupplyManagerDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateSupplyManagerDto updateSupplyManager)
        {
            await Mediator.Send(_mapper.Map<UpdateSupplyManagerDto,
                UpdateSupplyManagerCommand>(updateSupplyManager));

            return NoContent();
        }

        /// <summary>
        /// Deletes the supply manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/supplymanager/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">SupplyManager id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteSupplyManagerCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restore the supply manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/1.0/supplymanager/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">SupplyManager id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreSupplyManagerCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
