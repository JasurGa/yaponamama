using System;
using Atlas.Application.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atlas.WebApi.Filters
{
    public class AuthRoleFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string[] RoleClaims { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }

            var hasAny = false;
            foreach (var role in RoleClaims)
            {
                try
                {
                    var adminId = context.HttpContext.User.FindFirst(role).Value;
                    hasAny = true;
                    break;
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
            }

            if (!hasAny)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}