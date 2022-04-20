using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class ProfileController : BaseController
    {
        private readonly IAtlasDbContext _dbContext;

        public ProfileController(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/profile
        /// </remarks>
        /// <returns>Returns UserDetailsVm object</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDetailsVm>> GetUserProfileAsync()
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery
            {
                Id = UserId
            });

            return Ok(vm);
        }
    }
}
