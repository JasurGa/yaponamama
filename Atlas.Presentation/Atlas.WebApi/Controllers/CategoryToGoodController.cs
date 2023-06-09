﻿using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoriesToGood;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateManyCategoryToGood;
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
        ///     
        ///     GET /api/1.0/categorytogood/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8?showDeleted=false
        ///     
        /// </remarks>
        /// <returns>Returns CategoryToGoodListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("good/{goodId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CategoryListVm>> GetByGoodIdAsync([FromRoute] Guid goodId,
            [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new GetCategoryToGoodListByGoodIdQuery 
            { 
                GoodId      = goodId,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches categories to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/categorytogood
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "categoryIds": [
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCategoriesToGood">CreateCategoriesToGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("many")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategoriesToGoodDto createCategoriesToGood)
        {
            await Mediator.Send(_mapper.Map<CreateCategoriesToGoodDto,
                CreateCategoriesToGoodCommand>(createCategoriesToGood));

            return NoContent();
        }

        /// <summary>
        /// Attaches categories to goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/categorytogood/manytomany
        ///     {
        ///         [
        ///             "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "categoryIds": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCategoriesToGoods">CreateCategoriesToGoodsDto object</param>
        /// <returns>Returns NoContent</returns> 
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("manytomany")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategoriesToGoodsDto createCategoriesToGoods)
        {
            await Mediator.Send(_mapper.Map<CreateCategoriesToGoodsDto,
                CreateManyCategoryToGoodCommand>(createCategoriesToGoods));

            return NoContent();
        }

        /// <summary>
        /// Attaches category to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/categorytogood
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "categoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCategoryToGood">CreateCategoryToGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategoryToGoodDto createCategoryToGood)
        {
            await Mediator.Send(_mapper.Map<CreateCategoryToGoodDto,
                CreateCategoryToGoodCommand>(createCategoryToGood));

            return NoContent();
        }

        /// <summary>
        /// Detaches good from category
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/categorytogood
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "categoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <param name="deleteCategoryToGood">DeleteCategoryToGoodDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(DeleteCategoryToGoodDto deleteCategoryToGood)
        {
            await Mediator.Send(_mapper.Map<DeleteCategoryToGoodDto,
                DeleteCategoryToGoodCommand>(deleteCategoryToGood));

            return NoContent();
        }
    }
}
