using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Atlas.WebApi.Middlewares
{
    public class LogUrlMiddleware
    {
        private readonly RequestDelegate  _next;

        public LogUrlMiddleware(RequestDelegate next, DiagnosticSource diagnostics)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return _next.Invoke(context);
        }
    }
}
