﻿using System;
using System.Security.Claims;
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
                    var roleId = context.HttpContext.User.FindFirstValue(role + "Id");
                    if (roleId != null)
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