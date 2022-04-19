using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class CategoryController : BaseController
    {
        /// <summary>
        /// Get the category by id
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
        /// Get the list of categories
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/category
        /// </remarks>
        /// <returns>Returns CategoryListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetCategoryListQuery());
            return Ok(vm);
        }
    }
}
