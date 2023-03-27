using System;
using Atlas.Application.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(TokenClaims.UserId).Value);

        internal Guid ClientId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(TokenClaims.ClientId).Value);

        internal Guid CourierId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(TokenClaims.CourierId).Value);

        internal bool IsAuthenticated => User.Identity.IsAuthenticated;
    }
}
