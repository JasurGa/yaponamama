using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.CreatePromoAdvertiseGood;
using Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.DeletePromoAdvertiseGood;
using Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage;
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
    public class PromoAdvertiseGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoAdvertiseGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a new promo advertise good object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/promoadvertisegood
        ///     {
        ///         "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "promoAdvertisePageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <param name="createPromoAdvertiseGoodDto">CreatePromoAdvertiseGoodDto object</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync(CreatePromoAdvertiseGoodDto createPromoAdvertiseGoodDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePromoAdvertiseGoodDto,
                CreatePromoAdvertiseGoodCommand>(createPromoAdvertiseGoodDto));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes a promo advertise good object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/1.0/promoadvertisegood
        ///     {
        ///         "goodId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "promoAdvertisePageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="deletePromoAdvertiseGoodDto">DeletePromoAdvertiseGoodDto object</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(DeletePromoAdvertiseGoodDto deletePromoAdvertiseGoodDto)
        {
            await Mediator.Send(_mapper.Map<DeletePromoAdvertiseGoodDto,
                DeletePromoAdvertiseGoodCommand>(deletePromoAdvertiseGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Get good ids by promo advertise page id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promoadvertisegood/promoadvertisepage/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// 
        /// </remarks>
        /// <returns>Returns GoodIdsListVm object</returns>
        /// <param name="id">PromoAdvertisePage id (Guid)</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("promoadvertisepage/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPromoAdvertisePageAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetGoodsByPromoAdvertisePageQuery
            {
                PromoAdvertisePageId = id
            });

            return Ok(vm);
        }
    }
}

