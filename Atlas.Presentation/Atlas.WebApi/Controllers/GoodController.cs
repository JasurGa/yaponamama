using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Goods.Commands.CreateGood;
using Atlas.Application.CQRS.Goods.Commands.DeleteGood;
using Atlas.Application.CQRS.Goods.Commands.RestoreGood;
using Atlas.Application.CQRS.Goods.Commands.UpdateGood;
using Atlas.Application.CQRS.Goods.Queries.GetGoodCounts;
using Atlas.Application.CQRS.Goods.Queries.GetGoodDetails;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedList;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodWithDiscountPagedList;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class GoodController : BaseController
    {
        private readonly IMapper _mapper;

        public GoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get goods count by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/count/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <returns>Returns int</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("count/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetGoodsCountByCategoryIdAsync(
            [FromRoute] Guid categoryId)
        {
            var vm = await Mediator.Send(new GetGoodCountsQuery
            {
                CategoryId = categoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of good by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8?showDeleted=true
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <returns>Returns GoodListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoodListVm>> GetGoodsByCategoryIdAsync([FromRoute]
            Guid categoryId, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGoodListByCategoryQuery
            {
                ShowDeleted = showDeleted,
                CategoryId  = categoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of good by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/paged/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8?pageSize=10&amp;pageIndex=0&amp;showDelete=false
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="showDeleted">Show deleted list</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetGoodsByCategoryIdAsync(
            [FromRoute] Guid categoryId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGoodPagedListByCategoryQuery
            {
                CategoryId = categoryId,
                PageIndex = pageIndex,
                PageSize = pageSize,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of good which has discount
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/discounted/paged?pageSize=10&amp;pageIndex=0&amp;showDeleted=false
        /// </remarks>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetAllAsync(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGoodPagedListQuery
            {
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of good which has discount
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/discounted/paged?pageSize=10&amp;pageIndex=0
        /// </remarks>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("discounted/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetGoodsWithDiscountAsync(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetGoodWithDiscountPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the good by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Good id (guid)</param>
        /// <returns>Returns GoodDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GoodDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetGoodDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new good
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/good
        ///     {
        ///         "name": "Sample name",
        ///         "description": "Sample description",
        ///         "photoPath": "/main/dir",
        ///         "sellingPrice": 100,
        ///         "purchasePrice": 99,
        ///         "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     }
        ///     
        /// </remarks>
        /// <param name="createGood">CreateGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateGoodDto createGood)
        {
            var goodId = await Mediator.Send(_mapper.Map<CreateGoodDto,
                CreateGoodCommand>(createGood));

            return Ok(goodId);
        }

        /// <summary>
        /// Updates the good
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/good
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "name": "Sample name",
        ///         "description": "Sample description",
        ///         "photoPath": "/main/dir",
        ///         "sellingPrice": 100,
        ///         "purchasePrice": 99,
        ///         "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     }
        /// 
        /// </remarks>
        /// <param name="updateGood">UpdateGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdateGoodDto updateGood)
        {
            await Mediator.Send(_mapper.Map<UpdateGoodDto,
                UpdateGoodCommand>(updateGood));

            return NoContent();
        }

        /// <summary>
        /// Deletes the good by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Good id</param>
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
            await Mediator.Send(new DeleteGoodCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the good by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Good id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreGoodCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
