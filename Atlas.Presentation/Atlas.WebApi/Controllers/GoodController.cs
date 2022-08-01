using System;
using AutoMapper;
using Atlas.WebApi.Models;
using Atlas.WebApi.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Goods.Commands.CreateGood;
using Atlas.Application.CQRS.Goods.Commands.DeleteGood;
using Atlas.Application.CQRS.Goods.Commands.RestoreGood;
using Atlas.Application.CQRS.Goods.Commands.UpdateGood;
using Atlas.Application.CQRS.Goods.Queries.GetGoodCounts;
using Atlas.Application.CQRS.Goods.Queries.GetGoodDetails;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByProvider;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedList;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodWithDiscountPagedList;
using Atlas.Application.CQRS.Goods.Queries.GetTopGoods;
using Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories;

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
        /// Get random goods by main category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/random/main
        /// </remarks>
        /// <returns>Returns TopGoodListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("random/main")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TopGoodListVm>> GetRandomGoodsAsync()
        {
            var vm = await Mediator.Send(new GetGoodsForMainCategoriesQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get random goods by main category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/random/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <returns>Returns TopGoodListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("random/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TopGoodListVm>> GetRandomGoodsAsync(
            [FromRoute] Guid categoryId)
        {
            var vm = await Mediator.Send(new GetTopGoodsQuery
            {
                CategoryId = categoryId
            });

            return Ok(vm);
        }

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
        /// Gets the list of goods by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/good/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8?showDeleted=true
        ///     
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <param name="showDeleted">Show deleted list of goods (bool)</param>
        /// <returns>Returns GoodListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoodListVm>> GetGoodsByCategoryIdAsync(
            [FromRoute] Guid categoryId, 
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGoodListByCategoryQuery
            {
                ShowDeleted = showDeleted,
                CategoryId  = categoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of goods by provider id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/good/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8?showDeleted=true
        ///     
        /// </remarks>
        /// <param name="providerId">Provider id (guid)</param>
        /// <param name="showDeleted">Show deleted list of goods (bool)</param>
        /// <returns>Returns GoodListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("provider/{providerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoodListVm>> GetGoodsByProviderIdAsync(
            [FromRoute] Guid providerId, 
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGoodListByProviderQuery
            {
                ShowDeleted = showDeleted,
                ProviderId  = providerId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of goods by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/good/paged/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8?pageSize=10&amp;pageIndex=0&amp;showDeleted=false&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetGoodsByCategoryIdAsync(
            [FromRoute] Guid categoryId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool showDeleted = false,
            [FromQuery] string sortable = "Name",
            [FromQuery] bool ascending = true)
        {
            var vm = await Mediator.Send(new GetGoodPagedListByCategoryQuery
            {
                CategoryId  = categoryId,
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                ShowDeleted = showDeleted,
                Sortable    = sortable,
                Ascending   = ascending,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/good/paged?pageSize=10&amp;pageIndex=0&amp;showDeleted=false&amp;sortable=Name&amp;ascending=true
        /// 
        /// </remarks>
        /// <param name="pageSize">Page size</param>
        /// <param name="showDeleted">Show deleted records</param>
        /// <param name="sortable">Field to order the records by</param>
        /// <param name="ascending">Type of ordering records ("Ascending" || "Descending")</param>
        /// <param name="search">Search string</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetAllAsync(
            [FromQuery] int pageIndex    = 0, 
            [FromQuery] int pageSize     = 10,
            [FromQuery] bool showDeleted = false,
            [FromQuery] string sortable  = "Name",
            [FromQuery] bool ascending   = true,
            [FromQuery] string search    = "")
        {
            var vm = await Mediator.Send(new GetGoodPagedListQuery
            {
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                ShowDeleted = showDeleted,
                Sortable    = sortable,
                Ascending   = ascending,
                Search      = search,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of good which has discount
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/good/discounted/paged?pageSize=10&amp;pageIndex=0&amp;showDeleted=true&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="showDeleted">Show whether deleted or not deleted records</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("discounted/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetGoodsWithDiscountAsync(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool showDeleted = false,
            [FromQuery] string sortable = "Name",
            [FromQuery] bool ascending = true)
        {
            var vm = await Mediator.Send(new GetGoodWithDiscountPagedListQuery
            {
                PageIndex   = pageIndex,
                PageSize    = pageSize,
                ShowDeleted = showDeleted,
                Sortable    = sortable,
                Ascending   = ascending
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the good by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
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
        ///         "name": "Маленькая бутылка Pepsi",
        ///         "description": "Абсолютно такая же как и кола",
        ///         "photoPath": "/storage/goods/small-pepsi/img.jpg",
        ///         "sellingPrice": 6000,
        ///         "purchasePrice": 4000,
        ///         "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "discount": 1000,
        ///         "mass": 0,
        ///         "volume": 0.5,
        ///     }
        /// 
        /// </remarks>
        /// <param name="createGood">CreateGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        ///         "name": "Маленькая бутылка Coca-cola",
        ///         "description": "Абсолютно такая же как и пепси",
        ///         "photoPath": "/storage/goods/small-coca-cola/img.jpg",
        ///         "sellingPrice": 6000,
        ///         "purchasePrice": 3500,
        ///         "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "mass": 0,
        ///         "volume": 0.5,
        ///         "discount": 1000
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
        /// 
        ///     DELETE /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
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
        /// 
        ///     PATCH /api/1.0/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
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
