using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Stores.Commands.CreateStore;
using Atlas.Application.CQRS.Stores.Commands.DeleteStore;
using Atlas.Application.CQRS.Stores.Commands.RestoreStore;
using Atlas.Application.CQRS.Stores.Commands.UpdateStore;
using Atlas.Application.CQRS.Stores.Queries.GetStoreDetails;
using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.CQRS.Stores.Queries.GetStorePagedList;
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
        /// 
        ///     GET /api/1.0/store?showDeleted=true
        ///     
        /// </remarks>
        /// <returns>Returns StoreListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support, Roles.Client })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreListVm>> GetAllAsync([FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetStoreListQuery()
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of stores
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/store/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto StoreLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<StoreLookupDto>>> GetAllPagedAsync(
            [FromQuery] bool   showDeleted = false,
            [FromQuery] int    pageIndex   = 0,
            [FromQuery] int    pageSize    = 10,
            [FromQuery] string sortable    = "Name",
            [FromQuery] bool   ascending   = true)
        {
            var vm = await Mediator.Send(new GetStorePagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                Sortable    = sortable,
                Ascending   = ascending,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the store details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <returns>Returns StoreDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
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
        /// 
        ///     DELETE /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
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
        /// 
        ///     POST /api/1.0/store
        ///     {
        ///         "name": "Чиланзарский склад",
        ///         "nameRu": "Чиланзарский склад",
        ///         "nameEn": "Чиланзарский склад",
        ///         "nameUz": "Чиланзарский склад",
        ///         "address": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressRu": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressEn": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressUz": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "phoneNumber": "+998901234567",
        ///         "longitude": 41.657,
        ///         "longitude": -12.654
        ///     }
        ///     
        /// </remarks>
        /// <param name="createStore">CreateStoreDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateStoreDto createStore)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateStoreDto,
                CreateStoreCommand>(createStore));

            return Ok(vm);
        }

        /// <summary>
        /// Updates the store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/store
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "name": "Чиланзарский склад",
        ///         "nameRu": "Чиланзарский склад",
        ///         "nameEn": "Чиланзарский склад",
        ///         "nameUz": "Чиланзарский склад",
        ///         "address": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressRu": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressEn": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "addressUz": "Ташкент, Чиланзарский район, улица им.Низами, 12Б",
        ///         "phoneNumber": "+998901234567",
        ///         "longitude": 32.123,
        ///         "longitude": -12.9878,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateStore">UpdateStoreDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateStoreDto updateStore)
        {
            await Mediator.Send(_mapper.Map<UpdateStoreDto,
                UpdateStoreCommand>(updateStore));
            return NoContent();
        }

        /// <summary>
        /// Restore the specific store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /api/1.0/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Store id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreStoreCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
