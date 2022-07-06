using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Atlas.Identity.Middlewares
{
    public class OptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public OptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Options)
            {
                return _next.Invoke(context);
            }

            context.Response.StatusCode = 200;

            context.Response.Headers.Add("Access-Control-Allow-Origin", new[]
            {
                (string)context.Request.Headers["Origin"]
            });

            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] {
                $"Origin, X-Requested-With, Content-Type, Accept, Authorization, x-signalr-user-agent"
            });

            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] {
                HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete,
                HttpMethods.Options, HttpMethods.Patch
            });

            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] {
                "true"
            });

            return context.Response.WriteAsync(string.Empty);
        }
    }
}
