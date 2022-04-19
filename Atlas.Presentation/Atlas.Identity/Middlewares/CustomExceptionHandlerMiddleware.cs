using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using FluentValidation;

namespace Atlas.Identity.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code   = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new
                {
                    error = exception.Message
                });
            }

            return context.Response.WriteAsync(result);
        }
    }

}
