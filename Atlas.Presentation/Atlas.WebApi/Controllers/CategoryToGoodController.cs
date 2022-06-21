using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood;
using Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood;
using Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId;
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
    public class CategoryToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public CategoryToGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of categories to goods by good id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/categorytogood/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <returns>Returns CategoryToGoodListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("good/{goodId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryToGoodListVm>> GetByGoodIdAsync([FromRoute] Guid goodId)
        {
            var vm = await Mediator.Send(new GetCategoryToGoodListByGoodIdQuery 
            { 
                GoodId = goodId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches category to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/categorytogood
        /// {
        ///     "GoodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "CategoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        /// }
        /// </remarks>
        /// <param name="createCategoryToGood">CreateCategoryToGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCategoryToGoodDto createCategoryToGood)
        {
            var categoryToGoodId = await Mediator.Send(_mapper
                .Map<CreateCategoryToGoodCommand>(createCategoryToGood));

            return Ok(categoryToGoodId);
        }

        /// <summary>
        /// Detaches good from category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/categorytogood/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">CategoryToGood id</param>
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
            await Mediator.Send(new DeleteCategoryToGoodCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
