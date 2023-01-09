using System;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoriesToGood;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Atlas.Application.CQRS.PhotoToGoods.Commands.CreatePhotoToGood;
using Atlas.Application.CQRS.PhotoToGoods.Commands.DeletePhotoToGood;
using Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PhotoToGoodsController : BaseController
	{
        private readonly IMapper _mapper;

        public PhotoToGoodsController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a new photo to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/phototogoods
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "photoPath": "photo.jpg"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns Guid</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreatePhotoToGoodDto createPhotoToGoodDto)
        {
            await Mediator.Send(_mapper.Map<CreatePhotoToGoodDto,
                CreatePhotoToGoodCommand>(createPhotoToGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes a photo to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/phototogoods/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeletePhotoToGoodCommand
            {
                PhotoToGoodId = id
            });

            return NoContent();
        }

        /// <summary>
        /// Get photos by good id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/phototogoods/good/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <returns>Returns PhotoToGoodListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("good/{goodId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoToGoodListVm>> GetByGoodIdAsync([FromRoute] Guid goodId)
        {
            var vm = await Mediator.Send(new GetPhotosByGoodIdQuery
            {
                GoodId = goodId
            });

            return Ok(vm);
        }
    }
}

