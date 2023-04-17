using System;
using Atlas.Application.Common.Constants;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryDetails;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryPagedList;
using Atlas.Application.CQRS.PromoCategories.Commands.CreatePromoCategory;
using Atlas.Application.CQRS.PromoCategories.Commands.UpdatePromoCategory;
using Atlas.Application.CQRS.PromoCategories.Commands.DeletePromoCategory;
using Atlas.Application.CQRS.PromoCategories.Commands.RestorePromoCategory;
using Atlas.Application.CQRS.PromoCategories.Commands.CreateManyPromoCategories;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PromoCategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoCategoryController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the promo category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/promocategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">PromoCategory id (guid)</param>
        /// <returns>Returns PromoCategoryDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PromoCategoryDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetPromoCategoryDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of promo categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/promocategory?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PromoCategoryListVm>> GetAllAsync(
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetPromoCategoryListQuery
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of promo categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/promocategory/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto PromoCategoryLookupDto object</returns>
        /// <response code="200">Success</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageDto<PromoCategoryLookupDto>>> GetAllPagedAsync(
            [FromQuery] bool showDeleted = false,
            [FromQuery] int pageIndex    = 0,
            [FromQuery] int pageSize     = 10)
        {
            var vm = await Mediator.Send(new GetPromoCategoryPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new promo category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/promocategory
        ///     {
        ///         "nameRu": "Sample name of category",
        ///         "nameEn": "Sample name of category",
        ///         "nameUz": "Sample name of category",
        ///         "imageUrl": "/0123456789abcdef0123456789abcdef.png"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createPromoCategory">CreatePromoCategoryDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePromoCategoryDto createPromoCategory)
        {
            var categoryId = await Mediator.Send(_mapper.Map<CreatePromoCategoryDto,
                CreatePromoCategoryCommand>(createPromoCategory));

            return Ok(categoryId);
        }

        /// <summary>
        /// Creates a bunch of new promo categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/promocategory/many
        ///     [
        ///         {
        ///             "nameRu": "Sample name of category",
        ///             "nameEn": "Sample name of category",
        ///             "nameUz": "Sample name of category",
        ///             "imageUrl": "/0123456789abcdef0123456789abcdef.png"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <param name="createManyPromoCategories">CreateManyPromoCategoriesDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("many")]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateManyAsync([FromBody] CreateManyPromoCategoriesDto createManyPromoCategories)
        {
            var promoCategoryIds = await Mediator.Send(_mapper.Map<CreateManyPromoCategoriesDto,
                CreateManyPromoCategoriesCommand>(createManyPromoCategories));

            return Ok(promoCategoryIds);
        }

        /// <summary>
        /// Updates the promo category
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/promocategory
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "nameRu": "Sample name of category",
        ///         "nameEn": "Sample name of category",
        ///         "nameUz": "Sample name of category",
        ///         "imageUrl": "/0123456789abcdef0123456789abcdef.png",
        ///     }
        ///     
        /// </remarks>
        /// <param name="updatePromoCategory">UpdatePromoCategoryDto object</param>
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
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdatePromoCategoryDto updateCategory)
        {
            await Mediator.Send(_mapper.Map<UpdatePromoCategoryDto,
                UpdatePromoCategoryCommand>(updateCategory));

            return NoContent();
        }

        /// <summary>
        /// Deletes the promo category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/promocategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Promo category id</param>
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
            await Mediator.Send(new DeletePromoCategoryCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the promo category by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH /api/1.0/promocategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Promo category id</param>
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
        public async Task<IActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestorePromoCategoryCommand
            {
                Id = id,
            });

            return NoContent();
        }

    }
}

