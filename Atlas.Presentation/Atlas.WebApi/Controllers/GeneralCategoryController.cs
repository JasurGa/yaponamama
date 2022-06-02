using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.GeneralCategories.Commands.CreateGeneralCategory;
using Atlas.Application.CQRS.GeneralCategories.Commands.DeleteGeneralCategory;
using Atlas.Application.CQRS.GeneralCategories.Commands.RestoreGeneralCategory;
using Atlas.Application.CQRS.GeneralCategories.Commands.UpdateGeneralCategory;
using Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories;
using Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategoryById;
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
    public class GeneralCategoryController : BaseController
    {
        private readonly IMapper _mapper;

        public GeneralCategoryController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates the general category
        /// </summary>
        /// <remarks>
        /// POST /api/1.0/generalcategory
        /// {
        ///     "name": "New category"
        /// }
        /// </remarks>
        /// <param name="createGeneralCategoryDto">CreateGeneralCategoryDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync(CreateGeneralCategoryDto createGeneralCategoryDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateGeneralCategoryDto,
                CreateGeneralCategoryCommand>(createGeneralCategoryDto));

            return Ok(vm);
        }

        /// <summary>
        /// Gets the general category by id 
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/generalcategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">General category id (guid)</param>
        /// <returns>Returns GeneralCategoryLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GeneralCategoryLookupDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetGeneralCategoryByIdQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of general categories
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/generalcategory?showDeleted=false
        /// </remarks>
        /// <param name="showDeleted">Show deleted (bool)</param>
        /// <returns>Returns GeneralCategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GeneralCategoriesListVm>> GetAllAsync([FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetGeneralCategoriesQuery
            {
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the general category by id
        /// </summary>
        /// <remarks>
        /// PUT /api/1.0/generalcategory
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "name": "Updated category"
        /// }
        /// </remarks>
        /// <param name="updateGeneralCategoryDto">UpdateGeneralCategoryDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateGeneralCategoryDto updateGeneralCategory)
        {
            await Mediator.Send(_mapper.Map<UpdateGeneralCategoryDto,
                UpdateGeneralCategoryCommand>(updateGeneralCategory));

            return NoContent();
        }

        /// <summary>
        /// Deletes the general category by id
        /// </summary>
        /// <remarks>
        /// DELETE /api/1.0/generalcategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">General category id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is uauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteGeneralCategoryCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the deleted general category by id
        /// </summary>
        /// <remarks>
        /// PATCH /api/1.0/generalcategory/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">General category id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is uauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreGeneralCategoryCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
