using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Couriers.Commands.CreateCourier;
using Atlas.Application.CQRS.Couriers.Commands.DeleteCourier;
using Atlas.Application.CQRS.Couriers.Commands.RestoreCourier;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCourier;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCouriersStoreId;
using Atlas.Application.CQRS.Couriers.Queries.FindCourierPagedList;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierByVehicleId;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedListByStoreId;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedListNotByStoreId;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class CourierController : BaseController
    {
        private readonly IMapper _mapper;

        public CourierController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search couriers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/courier/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto ClientLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<CourierLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new FindCourierPagedListQuery
            {
                ShowDeleted = showDeleted,
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of couriers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/courier/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto CourierLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CourierLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetCourierPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of couriers by vehicle ids
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/courier/vehicle/a3eb7b4a-9f4e-4c71-8619-398655c563b8?&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <returns>Returns the list of CouierDetailsVm</returns>
        /// <response code="404">NotFound</response>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("vehicle/{vehicleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CourierDetailsVm>>> GetByVehicleAsync([FromRoute] Guid vehicleId,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCourierByVehicleIdQuery
            {
                VehicleId   = vehicleId,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of couriers by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/courier/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?&amp;showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto CourierLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{storeId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CourierLookupDto>>> GetAllPagedAsync([FromRoute] Guid storeId, [FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetCourierPagedListByStoreIdQuery
            {
                StoreId     = storeId,
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of all couriers except couriers which contains store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/courier/except/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?&amp;showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto CourierLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("except/store/{storeId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CourierLookupDto>>> GetNotByStoreIdPagedAsync(
            [FromRoute] Guid storeId,
            [FromQuery] bool showDeleted = false,
            [FromQuery] int  pageIndex   = 0,
            [FromQuery] int  pageSize    = 10)
        {
            var vm = await Mediator.Send(new GetCourierPagedListNotByStoreIdQuery
            {
                StoreId     = storeId,
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the courier details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/courier/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Courier id (guid)</param>
        /// <returns>Returns CourierDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CourierDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetCourierDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/courier
        ///     {
        ///         "user": {
        ///             "login": "admin",
        ///             "password": "admin",
        ///             "firstName": "Ivan",
        ///             "lastName": "Ivan",
        ///             "middleName": "Ivanovich",
        ///             "sex": 0,
        ///             "birthday": "1990-01-01T10:00:00",
        ///             "avatarPhotoPath": "/0123456789abcdef0123456789abcdef.png",
        ///         },
        ///         "phoneNumber": "+998901234567"
        ///         "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///         "driverLicensePath": "/storage/driverLicensePath/dlp123.pdf",
        ///         "vehicleId": "y2u4h5j6-9f4e-4c71-8619-398655c563b8",
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCourier">CreateCourierDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="401">Not found</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCourierDto createCourier)
        {
            var courierId = await Mediator.Send(_mapper.Map<CreateCourierDto,
                CreateCourierCommand>(createCourier));

            return Ok(courierId);
        }

        /// <summary>
        /// Adds couriers to store 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/courier/store
        ///     {
        ///         "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "courierIds": [
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateCouriersStoreIdDto">UpdateCouriersStoreIdDto object</param>
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
        public async Task<ActionResult> UpdateStoreIdAsync([FromBody] UpdateCouriersStoreIdDto updateCouriersStoreIdDto)
        {
            await Mediator.Send(_mapper.Map<UpdateCouriersStoreIdDto,
                UpdateCouriersStoreIdCommand>(updateCouriersStoreIdDto));
            return NoContent();
        }

        /// <summary>
        /// Updates the courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/courier
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "user": {
        ///             "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "login": "admin",
        ///             "password": "admin",
        ///             "firstName": "Ivan",
        ///             "lastName": "Ivan",
        ///             "middleName": "Ivanovich",
        ///             "sex": 0,
        ///             "birthday": "1990-01-01T10:00:00",
        ///             "avatarPhotoPath": "/0123456789abcdef0123456789abcdef.png",
        ///         },
        ///         "phoneNumber": "+998901234567"
        ///         "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///         "driverLicensePath": "/storage/driverLicensePath/dlp123.pdf",
        ///         "vehicleId": "y2u4h5j6-9f4e-4c71-8619-398655c563b8",
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateCourier">UpdateCourierDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCourierDto updateCourier)
        {
            await Mediator.Send(_mapper.Map<UpdateCourierDto,
                UpdateCourierCommand>(updateCourier));

            return NoContent();
        }

        /// <summary>
        /// Deletes the courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/courier/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Courier id (guid)</param>
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
            await Mediator.Send(new DeleteCourierCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restore the specific courier
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH /api/1.0/courier/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Courier id (guid)</param>
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
            await Mediator.Send(new RestoreCourierCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
