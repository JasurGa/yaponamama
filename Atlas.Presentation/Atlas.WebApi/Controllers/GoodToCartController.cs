using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.GoodToCarts.Commands.CreateGoodToCart;
using Atlas.Application.CQRS.GoodToCarts.Commands.DeleteAllGoodToCarts;
using Atlas.Application.CQRS.GoodToCarts.Commands.DeleteGoodToCart;
using Atlas.Application.CQRS.GoodToCarts.Commands.UpdateGoodToCart;
using Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart;
using Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class GoodToCartController : BaseController
    {
        private readonly IMapper _mapper;

        public GoodToCartController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Fully changes client's shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/1.0/goodtocart/many
        ///     [
        ///         {
        ///             "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "count": 10
        ///         },
        ///         {
        ///             "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "count": 10
        ///         },
        ///         {
        ///             "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "count": 10
        ///         },
        ///     ]
        /// 
        /// </remarks>
        /// <returns>Returns LackingGoodListVm object</returns>
        /// <param name="updateGoodToCarts">List of UpdateGoodToCartDto objects</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code-"401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut("many")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LackingGoodListVm>> UpdateManyAsync([FromBody]
            List<UpdateGoodToCartDto> updateStoreToGoodDto)
        {
            var vm = await Mediator.Send(new UpdateManyGoodsInCartCommand
            {
                ClientId = ClientId,
                GoodsToCount = updateStoreToGoodDto.ToDictionary(x => x.Id,
                    x => x.Count)
            });

            return Ok(vm);
        }

        /// <summary>
        /// Adds a new good to client's shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/1.0/goottocart
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "count": 10,
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns id (guid)</returns>
        /// <param name="createGoodToCart">CreateGoodToCartDto object</param>
        /// <response code="202">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateGoodToCartDto createGoodToCart)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateGoodToCartDto,
                CreateGoodToCartCommand>(createGoodToCart, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Removes the good from client's shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/goodtocart/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">GoodToCart id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteGoodToCartCommand
            {
                Id = id,
                ClientId = ClientId,
            });

            return NoContent();
        }

        /// <summary>
        /// Removes all goods from client's shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/goodtocart/all
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("all")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAllAsync()
        {
            await Mediator.Send(new DeleteAllGoodToCartsCommand
            {
                ClientId = ClientId
            });

            return NoContent();
        }

        /// <summary>
        /// Updates the good from store relation
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/storetogood
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///         "count": 10,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateStoreToGoodDto">UpdateStoreToGoodDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody]
            UpdateGoodToCartDto updateStoreToGoodDto)
        {
            await Mediator.Send(_mapper.Map<UpdateGoodToCartDto,
                UpdateGoodToCartCommand>(updateStoreToGoodDto, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return NoContent();
        }

        /// <summary>
        /// Gets the list of goods in client's shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/goodtocart
        ///     
        /// </remarks>
        /// <returns>Returns GoodToCartListVm</returns>
        /// <response code="202">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GoodToCartListVm>> GetByClientAsync()
        {
            var vm = await Mediator.Send(new GetGoodToCartListQuery
            {
                ClientId = ClientId
            });

            return Ok(vm);
        }
    }
}
