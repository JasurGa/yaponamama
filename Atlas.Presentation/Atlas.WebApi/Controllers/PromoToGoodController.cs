using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood;
using Atlas.Application.CQRS.PromoToGoods.Commands.DeletePromoToGood;
using Atlas.Application.CQRS.PromoToGoods.Commands.UpdatePromoToGood;
using Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId;
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
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PromoToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoToGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a promo to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/promotogood
        ///     {
        ///         "promoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///     
        /// </remarks>
        /// <param name="createPromoToGoodDto">CreatePromoToGoodDto object</param>
        /// <returns>Returns promoToGood id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePromoToGoodDto createPromoToGoodDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePromoToGoodDto,
                CreatePromoToGoodCommand>(createPromoToGoodDto));

            return Ok(vm);
        }

        /// <summary>
        /// Gets a promos to goods
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/promotogood/promo/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     
        /// </remarks>
        /// <param name="promoId">Promo id (Guid)</param>
        /// <returns>Returns PromoToGoodListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("promo/{promoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoToGoodListVm>> GetByPromoAsync([FromRoute] Guid promoId)
        {
            var vm = await Mediator.Send(new GetPromoToGoodsByPromoIdQuery
            {
                PromoId = promoId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates a promo to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/promotogood
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "promoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///     
        /// </remarks>
        /// <param name="updatePromoToGoodDto">UpdatePromoToGoodDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePromoToGoodDto updatePromoToGoodDto)
        {
            await Mediator.Send(_mapper.Map<UpdatePromoToGoodDto,
                UpdatePromoToGoodCommand>(updatePromoToGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes a promo to good
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/promotogood/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     
        /// </remarks>
        /// <param name="id">PromoToGood id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeletePromoToGoodCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}
