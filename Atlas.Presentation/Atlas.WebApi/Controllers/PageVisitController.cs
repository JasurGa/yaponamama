using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atlas.Application.CQRS.PageVisits.Commands.IncrementPageVisit;
using System.Collections.Generic;
using Atlas.Application.CQRS.PageVisits.Queries.GetPagesVisits;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PageVisitController : BaseController
    {
        /// <summary>
        /// Incremenets count for page
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/pagevisit/increment
        /// "/good/1"
        /// </remarks>
        /// <returns>Returns current visit count (int)</returns>
        /// <param name="path">Path to the page</param>
        /// <response code="200">Success</response>
        [HttpPatch("increment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> IncremenetVisit([FromBody] string path)
        {
            var vm = await Mediator.Send(new IncrementPageVisitCommand
            {
                Path = path
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets visit count for pages
        /// </summary>
        /// <remarks>
        /// POST /api/pagevisit
        /// [
        ///     "/good/1",
        ///     "/good/2",
        ///     "/good/3",
        ///     "..."
        /// ]
        /// </remarks>
        /// <returns>Returns visit counts (list of ints)</returns>
        /// <param name="pages">Paths to the pages</param>
        /// <response code="200">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<int>>> GetVisitsAsync([FromBody] IList<string> pages)
        {
            var vm = await Mediator.Send(new GetPagesVisitsQuery
            {
                Pages = pages
            });

            return Ok(vm);
        }
    }
}
