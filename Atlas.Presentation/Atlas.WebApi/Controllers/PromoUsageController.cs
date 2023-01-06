using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PromoUsages.Queries.GetLastPromoUsagesPaged;
using Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PromoUsageController : BaseController
    {
        /// <summary>
        /// Get promo usages by client id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promousage/client/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="clientId">Client Id (Guid)</param>
        /// <returns>Returns PromoUsageListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        public async Task<ActionResult<PromoUsageListVm>> GetByClientIdAsync([FromRoute] Guid clientId)
        {
            var vm = await Mediator.Send(new GetPromoUsagesByClientIdQuery
            {
                ClientId = clientId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get my promo usages
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promousage/client/my
        ///     
        /// </remarks>
        /// <returns>Returns PromoUsageListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("client/my")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoUsageListVm>> GetMyAsync()
        {
            var vm = await Mediator.Send(new GetPromoUsagesByClientIdQuery
            {
                ClientId = ClientId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get last promo usages paged
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/promousage/paged?pageIndex=0&amp;pageSize=10
        /// 
        /// </remarks>
        /// <param name="pageSize">Page size (int)</param>
        /// <param name="pageIndex">Page index (int)</param>
        /// <returns>Returns PromoUsageListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Client })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<PromoUsageLookupDto>>> GetPagedAsync([FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetLastPromoUsagesPagedQuery
            {
                PageSize  = pageSize,
                PageIndex = pageIndex
            });

            return Ok(vm);
        }
    }
}

