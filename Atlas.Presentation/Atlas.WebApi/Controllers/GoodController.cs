using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class GoodController : BaseController
    {
        /// <summary>
        /// Get the list of good by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <returns>Returns GoodListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GoodListVm>> GetGoodsByCategoryIdAsync([FromRoute] Guid categoryId)
        {
            var vm = await Mediator.Send(new GetGoodListByCategoryQuery
            {
                CategoryId = categoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of good by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/good/paged/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8?pageSize=10&amp;pageIndex=0
        /// </remarks>
        /// <param name="categoryId">Category id (guid)</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Returns PageDto GoodLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<GoodLookupDto>>> GetGoodsByCategoryIdAsync([FromRoute] Guid categoryId,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetGoodPagedListByCategoryQuery
            {
                CategoryId = categoryId,
                PageIndex  = pageIndex,
                PageSize   = pageSize
            });

            return Ok(vm);
        }
    }
}
