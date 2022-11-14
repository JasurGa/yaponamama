using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoryToGood;
using Atlas.Application.CQRS.PromoCategoryToGoods.Commands.DeletePromoCategoryToGood;
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
    public class PromoCategoryToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoCategoryToGoodController(IMapper mapper) =>
            _mapper = mapper;


        /// <summary>
        /// Creates a new promo categories to goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/promocategorytogood/many
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "promoCategoryIds": ["a3eb7b4a-9f4e-4c71-8619-398655c563b8"]
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="createPromoCategoriesToGoodDto">CreatePromoCategoriesToGoodDto object</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthozed</response>
        [Authorize]
        [HttpPost("many")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateManyAsync([FromBody] CreatePromoCategoriesToGoodDto createPromoCategoriesToGoodDto)
        {
            await Mediator.Send(_mapper.Map<CreatePromoCategoriesToGoodDto,
                CreatePromoCategoriesToGoodCommand>(createPromoCategoriesToGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Creates a new promo category to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/promocategorytogood
        ///     {
        ///         "promoCategoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <param name="createPromoCategoryToGoodDto">CreatePromoCategoryToGoodDto object</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthozed</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePromoCategoryToGoodDto createPromoCategoryToGoodDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePromoCategoryToGoodDto,
                CreatePromoCategoryToGoodCommand>(createPromoCategoryToGoodDto));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes a promo category to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/1.0/promocategorytogood
        ///     {
        ///         "promoCategoryId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="deletePromoCategoryToGoodDto">DeletePromoCategoryToGoodDto object</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromBody] DeletePromoCategoryToGoodDto deletePromoCategoryToGoodDto)
        {
            await Mediator.Send(_mapper.Map<DeletePromoCategoryToGoodDto,
                DeletePromoCategoryToGoodCommand>(deletePromoCategoryToGoodDto));

            return NoContent();
        }
    }
}

