using System;
using System.Security.Claims;
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

        internal Guid UserId
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return Guid.Empty;
                }

                var cl = User.FindFirst(TokenClaims.UserId);
                if (cl == null || cl.Value == null)
                {
                    return Guid.Empty;
                }

                return Guid.Parse(cl.Value);
            }
        }

        internal Guid ClientId 
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return Guid.Empty;
                }

                var cl = User.FindFirst(TokenClaims.ClientId);
                if (cl == null || cl.Value == null)
                {
                    return Guid.Empty;
                }

                return Guid.Parse(cl.Value);
            }
        }

        internal Guid CourierId
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return Guid.Empty;
                }

                var cl = User.FindFirst(TokenClaims.CourierId);
                if (cl == null || cl.Value == null)
                {
                    return Guid.Empty;
                }

                return Guid.Parse(cl.Value);
            }
        }

        internal bool IsAuthenticated => User.Identity.IsAuthenticated;
    }
}
