using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using Atlas.Application.CQRS.Categories.Commands.DeleteCategory;
using Atlas.Application.CQRS.Categories.Commands.RestoreCategory;
using Atlas.Application.CQRS.Categories.Commands.UpdateCategory;
using Atlas.Application.CQRS.Categories.Queries.GetCategoriesByGeneralCategory;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList;
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
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Category id (guid)</param>
        /// <returns>Returns CategoryDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetCategoryDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/category?showDeleted=false
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetAllAsync(
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoryListQuery()
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of categories by general category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/category?showDeleted=false
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet("generalcategory/{generalCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetAllAsync([FromRoute] Guid generalCategoryId,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoriesByGeneralCategoryQuery
            {
                GeneralCategoryId = generalCategoryId,
                ShowDeleted       = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/category/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto CategoryLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CategoryLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetCategoryPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/category
        /// {
        ///     "name": "Sample name of category",
        /// }
        /// </remarks>
        /// <param name="createCategory">CreateCategoryDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [AuthRoleFilter(TokenClaims.AdminId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCategoryDto createCategory)
        {
            var categoryId = await Mediator.Send(_mapper.Map<CreateCategoryDto,
                CreateCategoryCommand>(createCategory));

            return Ok(categoryId);
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/category
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "name": "Sample category name",
        /// }
        /// </remarks>
        /// <param name="updateCategory">UpdateCategoryDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdateCategoryDto updateCategory)
        {
            await Mediator.Send(_mapper.Map<UpdateCategoryDto,
                UpdateCategoryCommand>(updateCategory));

            return NoContent();
        }

        /// <summary>
        /// Deletes the category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Category id</param>
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
            await Mediator.Send(new DeleteCategoryCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Category id</param>
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
        public async Task<IActionResult> RestoreAsync(Guid id)
        {
            await Mediator.Send(new RestoreCategoryCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
