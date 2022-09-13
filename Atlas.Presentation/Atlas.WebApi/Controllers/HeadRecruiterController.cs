using System;
using Atlas.Application.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList;
using Atlas.WebApi.Filters;
using Atlas.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class HeadRecruiterController : BaseController
    {
        private readonly IMapper _mapper;

        public HeadRecruiterController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search admins
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/headrecruiter/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <param name="showDeleted">Show deleted (bool)</param>
        /// <returns>Returns PageDto ClientLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<HeadRecruiterLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new FindHeadRecruitersPagedListQuery
            {
                ShowDeleted = showDeleted,
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }
    }
}

