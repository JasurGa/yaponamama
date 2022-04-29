using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood;
using Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood;
using Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId;
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
        /// Get the list of categories with goods by good id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/categoryToGoods/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <returns>Returns CategoryToGoodListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("good/{goodId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryToGoodListVm>> GetAllAsync(Guid goodId)
        {
            var vm = await Mediator.Send(new GetCategoryToGoodListByGoodIdQuery 
            { 
                GoodId = goodId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches category and good
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/categoryToGood
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCategoryToGoodDto createCategoryToGood)
        {
            var command = _mapper.Map<CreateCategoryToGoodCommand>(createCategoryToGood);

            var categoryToGoodId = Mediator.Send(command);

            return Ok(categoryToGoodId);
        }

        /// <summary>
        /// Deataches good and category by CategoryToGood relation id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/categoryToGood/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">CategoryToGood id</param>
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
            await Mediator.Send(new DeleteCategoryToGoodCommand
            {
                Id = id,
            });

            return NoContent();
        }

    }
}
