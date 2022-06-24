using System;
using Microsoft.AspNetCore.Builder;

namespace Atlas.Identity.Middlewares
{
    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseOptionsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<OptionsMiddleware>();
        }
    }
}
