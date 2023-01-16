using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Queries.FindStoreToGoodPagedList;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodListByStoreId;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId;
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
    public class StoreToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public StoreToGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search for goods in a store
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/storetogood/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/search?searchQuery=bla+bla+bla&amp;pageSize=10&amp;pageIndex=0
        ///     
        /// </remarks>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <returns>Returns PageDto GoodLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("store/{storeId}/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<StoreToGoodLookupDto>>> SearchAsync(
            [FromRoute] Guid storeId,
            [FromQuery] string searchQuery,
            [FromQuery] int pageSize = 10, 
            [FromQuery] int pageIndex = 0
        )
        {
            var vm = await Mediator.Send(new FindStoreToGoodPagedListQuery
            {
                SearchQuery = searchQuery,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                StoreId     = storeId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets list of goods by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/storetogood/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns StoreToGoodVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{storeId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreToGoodListVm>> GetAllByStoreIdAsync([FromRoute] Guid storeId)
        {
            var vm = await Mediator.Send(new GetStoreToGoodListByStoreIdQuery
            { 
                StoreId = storeId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets paged list of goods by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/storetogood/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8/paged?pageIndex=0&amp;pageSize=10&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="storeId">Store id (guid)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto StoreToGoodLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{storeId}/paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<StoreToGoodLookupDto>>> GetAllPagedByStoreIdAsync(
            [FromRoute] Guid storeId, 
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortable = "Id",
            [FromQuery] bool ascending = true,
            [FromQuery] bool ignoreNulls = false)
        {
            var vm = await Mediator.Send(new GetStoreToGoodPagedListByStoreIdQuery
            {
                StoreId     = storeId,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                Sortable    = sortable,
                Ascending   = ascending,
                IgnoreNulls = ignoreNulls
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches goods to store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/storetogood
        ///     {
        ///         "GoodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "Store":  "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "Count":  100,
        ///     }
        /// 
        /// </remarks>
        /// <param name="createStoreToGood">CreateStoreToGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateStoreToGoodDto createStoreToGood)
        {
            var storeToGoodId = await Mediator.Send(_mapper
                .Map<CreateStoreToGoodCommand>(createStoreToGood));

            return Ok(storeToGoodId);
        }

        /// <summary>
        /// Updates the good from store relation
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/storetogood
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         "count": 10,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateStoreToGoodDto">UpdateStoreToGoodDto object</param>
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
        public async Task<ActionResult> UpdateAsync([FromBody]
            UpdateStoreToGoodDto updateStoreToGoodDto)
        {
            await Mediator.Send(_mapper.Map<UpdateStoreToGoodDto, UpdateStoreToGoodCommand>
                (updateStoreToGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Detaches good from store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/storeToGood/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">StoreToGood id</param>
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
            await Mediator.Send(new DeleteStoreToGoodCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
