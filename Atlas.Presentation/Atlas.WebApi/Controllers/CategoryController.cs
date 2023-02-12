using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Categories.Commands.AddCategoryParent;
using Atlas.Application.CQRS.Categories.Commands.CreateCategory;
using Atlas.Application.CQRS.Categories.Commands.DeleteCategory;
using Atlas.Application.CQRS.Categories.Commands.RemoveCategoryParent;
using Atlas.Application.CQRS.Categories.Commands.RestoreCategory;
using Atlas.Application.CQRS.Categories.Commands.UpdateCategory;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryChildren;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryChildrenPagedList;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryParents;
using Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList;
using Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName;
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
        /// Search categories by good name
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/category/search
        ///     "search query"
        ///     
        /// </remarks>
        /// <returns>Returns SearchCategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchedCategoryListVm>> SearchCategoriesByGoodNameAsync([FromBody] string searchQuery)
        {
            var vm = await Mediator.Send(new SearchCategoriesByGoodNameQuery
            {
                SearchQuery = searchQuery
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of main categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/category/main?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns MainCategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet("main")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MainCategoryListVm>> GetAllMainAsync(
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetMainCategoryListQuery
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
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
        ///     
        ///     GET /api/1.0/category?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetAllAsync(
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoryListQuery
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of children categories of category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8/children?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}/children")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetChildrenCategoriesAsync([FromRoute] Guid id,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoryChildrenQuery
            {
                Id          = id,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of children categories of category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8/children/paged?showDeleted=false&amp;pageSize=10&amp;pageIndex=0
        /// 
        /// </remarks>
        /// <param name="id">Category id (guid)</param>
        /// <param name="showDeleted">Show deleted list or not</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto CategoryLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}/children/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CategoryLookupDto>>> GetChildrenCategoriesPagedAsync(
            [FromRoute] Guid id,
            [FromQuery] bool showDeleted = false,
            [FromQuery] int pageIndex    = 0,
            [FromQuery] int pageSize     = 10)
        {
            var vm = await Mediator.Send(new GetCategoryChildrenPagedListQuery
            {
                Id          = id,
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of parents of category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8/parent?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}/parent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetParentCategoriesAsync([FromRoute] Guid id,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoryParentsQuery
            {
                Id          = id,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/category/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10&amp;sortable=Name&amp;ascending=true
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortable">Property to sort</param>
        /// <param name="ascending">Sorting order: Ascending (true) || Descending (false)</param>
        /// <returns>Returns PageDto CategoryLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CategoryLookupDto>>> GetAllPagedAsync(
            [FromQuery] bool   showDeleted = false,
            [FromQuery] int    pageIndex   = 0,
            [FromQuery] int    pageSize    = 10,
            [FromQuery] string sortable   = "Name",
            [FromQuery] bool   ascending   = true)
        {
            var vm = await Mediator.Send(new GetCategoryPagedListQuery
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
        /// Add category parent
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/category/parent
        ///     {
        ///         "categoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "parentId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <param name="addCategoryParent">AddCategoryParentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("parent")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> AddCategoryParentAsync([FromBody] AddCategoryParentDto addCategoryParent)
        {
            await Mediator.Send(_mapper.Map<AddCategoryParentDto,
                AddCategoryParentCommand>(addCategoryParent));

            return NoContent();
        }

        /// <summary>
        /// Remove category parent
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/category/parent
        ///     {
        ///         "categoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "parentId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <param name="removeCategoryParent">RemoveCategoryParentDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("parent")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> RemoveCategoryParentAsync([FromBody] RemoveCategoryParentDto removeCategoryParent)
        {
            await Mediator.Send(_mapper.Map<RemoveCategoryParentDto,
                RemoveCategoryParentCommand>(removeCategoryParent));

            return NoContent();
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/category
        ///     {
        ///         "name": "Sample name of category",
        ///         "nameRu": "Sample name of category",
        ///         "nameEn": "Sample name of category",
        ///         "nameUz": "Sample name of category",
        ///         "imageUrl": "/0123456789abcdef0123456789abcdef.png",
        ///         "isMainCategory": true,
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCategory">CreateCategoryDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager})]
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
        /// 
        ///     PUT /api/1.0/category
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "nameRu": "Sample name of category",
        ///         "nameEn": "Sample name of category",
        ///         "nameUz": "Sample name of category",
        ///         "imageUrl": "/0123456789abcdef0123456789abcdef.png",
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateCategory">UpdateCategoryDto object</param>
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
        /// 
        ///     DELETE /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Category id</param>
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
        ///     
        ///     PATCH /api/1.0/category/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Category id</param>
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
