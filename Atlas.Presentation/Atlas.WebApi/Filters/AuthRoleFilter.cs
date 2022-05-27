using System;
using System.Collections.Generic;
using System.Security.Claims;
using Atlas.Application.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atlas.WebApi.Filters
{
    public class AuthRoleFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string[] RoleClaims { get; set; }

        public AuthRoleFilter(string roleClaim)
        {
            RoleClaims = new string[] { roleClaim };
        }

        public AuthRoleFilter(string[] roleClaims)
        {
            RoleClaims = roleClaims;
        }

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
                    var adminId = context.HttpContext.User.FindFirstValue(role);
                    if (adminId != null)
                    {
                        hasAny = true;
                        break;
                    }
                }
                catch (Exception e)
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