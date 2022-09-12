using System;
using Atlas.Application.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList;

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
        ///     GET /api/1.0/headrecruiter/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto ClientLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<HeadRecruiterLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new FindHeadRecruitersPagedListQuery
            {
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }
    }
}

