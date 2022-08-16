using Atlas.Application.CQRS.Languages.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class LanguageController : BaseController
    {
        /// <summary>
        /// Gets the list of languages
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/language
        ///     
        /// </remarks>
        /// <returns>Returns LanguageListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LanguageListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetLanguageListQuery());
            return Ok(vm);
        }
    }
}
