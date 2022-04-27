using Atlas.Application.CQRS.Vehicles.Commands.CreateVehicle;
using Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle;
using Atlas.Application.CQRS.Vehicles.Commands.UpdateVehicle;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedList;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListByStore;
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
    public class VehicleController : BaseController
    {
        private readonly IMapper _mapper;

        public VehicleController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get the list of vehicles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicle
        /// </remarks>
        /// <returns>Returns VehicleListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VehicleListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetVehicleListQuery());

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of vehicles by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicle/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="storeId">Store id (guid)</param>
        /// <returns>Returns VehicleListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("store/{storeId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VehicleListVm>> GetAllByStoreIdAsync(Guid storeId)
        {
            var vm = await Mediator.Send(new GetVehicleListByStoreQuery 
            { 
                StoreId = storeId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the vehicle by its id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicle/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Vehicle id (guid)</param>
        /// <returns>Returns VehicleDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VehicleDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetVehicleDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of vehicles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicle/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto VehicleLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<VehicleLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetVehiclePagedListQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of vehicles by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicle/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto VehicleLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("store/{storeId}/paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<VehicleLookupDto>>> GetAllPagedByStoreIdAsync(Guid storeId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetVehiclePagedListByStoreQuery
            {
                StoreId = storeId,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new vehicle
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/vehicle
        /// {
        ///     "name": "Sample name",
        ///     "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "vehicleTypeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "registrationNumber": "10FBI99",
        ///     "registrationCertificatePhotoPath": "/default/path"
        /// }
        /// </remarks>
        /// <param name="createVehicleDto">CreateVehicleDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateVehicleDto createVehicleDto)
        {
            var command = _mapper.Map<CreateVehicleCommand>(createVehicleDto);

            var vehicleId = await Mediator.Send(command);

            return Ok(vehicleId);
        }

        /// <summary>
        /// Updates the vehicle by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/vehicle
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "name": "Sample name",
        ///     "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "vehicleTypeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "registrationNumber": "10FBI99",
        ///     "registrationCertificatePhotoPath": "/default/path"
        /// }
        /// </remarks>
        /// <param name="updateVehicleDto">UpdatVehicleDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateVehicleDto updateVehicleDto)
        {
            var command = _mapper.Map<UpdateVehicleCommand>(updateVehicleDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific vehicle by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/vehicle/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Vehicle id</param>
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
            await Mediator.Send(new DeleteVehicleCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
