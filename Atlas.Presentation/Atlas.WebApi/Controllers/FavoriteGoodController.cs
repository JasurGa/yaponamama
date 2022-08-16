using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGood;
using Atlas.Application.CQRS.FavoriteGoods.Commands.CreateManyFavoriteGoods;
using Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGood;
using Atlas.Application.CQRS.FavoriteGoods.Queries.GetFavoritesByClientId;
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
    public class FavoriteGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public FavoriteGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates new favorite good 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/favoritegood
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns id (guid)</returns>
        /// <param name="createFavoriteGood">CreateFavoriteGoodDto object</param>
        /// <response code="202">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateFavoriteGoodDto createFavoriteGood)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateFavoriteGoodDto,
                CreateFavoriteGoodCommand>(createFavoriteGood, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Creates many favorite goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/favoritegood/many
        ///     {
        ///         "goodIds": [
        ///             a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns ids (guid)</returns>
        /// <param name="createFavoriteGoods">CreateFavoriteGoodsDto object</param>
        /// <response code="202">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("many")]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateManyAsync([FromBody] CreateFavoriteGoodsDto createFavoriteGoods)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateFavoriteGoodsDto,
                CreateFavoriteGoodsCommand>(createFavoriteGoods, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes favorite good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/favoritegood/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="id">FavoriteGoodId (Guid)</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteFavoriteGoodCommand
            {
                Id = id,
                ClientId = ClientId,
            });

            return NoContent();
        }

        /// <summary>
        /// Gets favorite good list
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/favoritegood
        ///     
        /// </remarks>
        /// <returns>Returns FavoriteGoodListVm</returns>
        /// <response code="202">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FavoriteGoodListVm>> GetByClientAsync()
        {
            var vm = await Mediator.Send(new GetFavoritesByClientIdQuery
            {
                ClientId = ClientId
            });

            return Ok(vm);
        }
    }
}
