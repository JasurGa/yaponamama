using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoAdvertises.Commands.CreatePromoAdvertise;
using Atlas.Application.CQRS.PromoAdvertises.Commands.DeletePromoAdvertise;
using Atlas.Application.CQRS.PromoAdvertises.Commands.UpdatePromoAdvertise;
using Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises;
using Atlas.Application.CQRS.PromoAdvertises.Queries.GetAllPagedPromoAdvertises;
using Atlas.Application.CQRS.Promos.Queries.GetPromoDetails;
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
    public class PromoAdvertiseController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoAdvertiseController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a new promo advertise
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/promoadvertise
        ///     {
        ///         "wideBackground": "/api/1.0/file/upload/wide.png",
        ///         "highBackground": "/api/1.0/file/upload/high.png",
        ///         "titleColor": "#ffffff",
        ///         "titleRu": "Привет",
        ///         "titleEn": "Hello",
        ///         "titleUz": "Salom",
        ///         "orderNumber": 1,
        ///         "expiresAt": "2022-11-11T10:00:00.000Z"
        ///     }
        /// 
        /// </remarks>
        /// <param name="createPromoAdvertiseDto">CreatePromoAdvertiseDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePromoAdvertiseDto createPromoAdvertiseDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePromoAdvertiseDto,
                CreatePromoAdvertiseCommand>(createPromoAdvertiseDto));

            return Ok(vm);
        }

        /// <summary>
        /// Updates a promo advertise
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/1.0/promoadvertise
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "wideBackground": "/api/1.0/file/upload/wide.png",
        ///         "highBackground": "/api/1.0/file/upload/high.png",
        ///         "titleColor": "#ffffff",
        ///         "titleRu": "Привет",
        ///         "titleEn": "Hello",
        ///         "titleUz": "Salom",
        ///         "orderNumber": 1,
        ///         "expiresAt": "2022-11-11T10:00:00.000Z"
        ///     }
        ///     
        /// </remarks>
        /// <param name="updatePromoAdvertiseDto">UpdatePromoAdvertiseDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePromoAdvertiseDto updatePromoAdvertiseDto)
        {
            var vm = await Mediator.Send(_mapper.Map<UpdatePromoAdvertiseDto,
                UpdatePromoAdvertiseCommand>(updatePromoAdvertiseDto));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes a promo advertise
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/promoadvertise/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// 
        /// </remarks>
        /// <param name="id">PromoAdvertise Id (Guid)</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new DeletePromoAdvertiseCommand
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get actual promo advertises
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promoadvertise/actual
        ///     
        /// </remarks>
        /// <returns>Returns PromoAdvertisesListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("actual")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PromoAdvertisesListVm>> GetActualAsync()
        {
            var vm = await Mediator.Send(new GetActualPromoAdvertisesQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get promo advertise by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promoadvertise/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">PromoAdvertise id (Guid)</param>
        /// <returns>Returns PromoAdvertiseDetailsVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PromoDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetActualPromoAdvertisesQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get all paged promo advertises
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promoadvertise?pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <returns>Returns PromoAdvertisesListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActualAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetAllPagedPromoAdvertisesQuery
            {
                PageSize  = pageSize,
                PageIndex = pageIndex
            });

            return Ok(vm);
        }
    }
}

