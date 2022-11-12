using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoAdvertisePages.Commands.CreatePromoAdvertisePage;
using Atlas.Application.CQRS.PromoAdvertisePages.Commands.DeletePromoAdvertisePage;
using Atlas.Application.CQRS.PromoAdvertisePages.Commands.UpdatePromoAdvertisePage;
using Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise;
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
    public class PromoAdvertisePageController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoAdvertisePageController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a new promo advertise page
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/promoadvertisepage
        ///     {
        ///         "promoAdvertiseId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "badgeColor": "#ffffff",
        ///         "badgeTextRu": "Привет",
        ///         "badgeTextEn": "Hello",
        ///         "badgeTextUz": "Salom",
        ///         "titleColor": "#ffffff",
        ///         "titleRu": "Привет",
        ///         "titleEn": "Hello",
        ///         "titleUz": "Salom",
        ///         "descriptionColor": "#ffffff",
        ///         "descriptionRu: "Привет",
        ///         "descriptionEn: "Hello",
        ///         "descriptionUz: "Salom",
        ///         "buttonColor": "#ffffff",
        ///         "background": "/api/1.0/file/download/background.png"
        ///     }
        ///
        /// </remarks>
        /// <param name="createPromoAdvertisePageDto">CreatePromoAdvertisePageDto object</param>
        /// <returns>Returns PromoAdvertisePage Id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePromoAdvertisePageDto createPromoAdvertisePageDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePromoAdvertisePageDto,
                CreatePromoAdvertisePageCommand>(createPromoAdvertisePageDto));

            return Ok(vm);
        }

        /// <summary>
        /// Gets promo advertise pages by the promo advertise id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/promoadvertisepage/promoadvertise/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">PromoAdvertisePage id (Guid)</param>
        /// <returns>Returns PromoAdvertisePageListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("promoadvertise/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPromoAdvertiseIdAsync([FromRoute] Guid promoAdvertiseId)
        {
            var vm = await Mediator.Send(new GetPagesByPromoAdvertiseQuery
            {
                PromoAdvertiseId = promoAdvertiseId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates a promo advertise page
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/1.0/promoadvertisepage
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "promoAdvertiseId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "badgeColor": "#ffffff",
        ///         "badgeTextRu": "Привет",
        ///         "badgeTextEn": "Hello",
        ///         "badgeTextUz": "Salom",
        ///         "titleColor": "#ffffff",
        ///         "titleRu": "Привет",
        ///         "titleEn": "Hello",
        ///         "titleUz": "Salom",
        ///         "descriptionColor": "#ffffff",
        ///         "descriptionRu: "Привет",
        ///         "descriptionEn: "Hello",
        ///         "descriptionUz: "Salom",
        ///         "buttonColor": "#ffffff",
        ///         "background": "/api/1.0/file/download/background.png"
        ///     }
        ///
        /// </remarks>
        /// <param name="updatePromoAdvertisePageDto">UpdatePromoAdvertisePageDto object</param>
        /// <returns>Returns PromoAdvertisePage Id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePromoAdvertisePageDto updatePromoAdvertisePageDto)
        {
            var vm = await Mediator.Send(_mapper.Map<UpdatePromoAdvertisePageDto,
                UpdatePromoAdvertisePageCommand>(updatePromoAdvertisePageDto));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes a promo advertise page
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/1.0/promoadvertisepage/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">PromoAdvertisePage id (Guid)</param>
        /// <returns>Returns NoContent</returns>
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
            var vm = await Mediator.Send(new DeletePromoAdvertisePageCommand
            {
                Id = id
            });

            return Ok(vm);
        }
    }
}

