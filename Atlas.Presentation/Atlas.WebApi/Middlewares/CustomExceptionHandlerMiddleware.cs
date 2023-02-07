using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Atlas.WebApi.Middlewares
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
                try
                {
                    result = JsonSerializer.Serialize(new
                    {
                        error      = exception.Message,
                        sourceFile = exception.Source,
                        stackTrace = exception.StackTrace,
                        inner = new
                        {
                            error      = exception.InnerException?.Message,
                            sourceFile = exception.InnerException?.Source,
                            stackTrace = exception.InnerException?.StackTrace
                        }
                    });
                }
                catch (Exception)
                {
                    result = JsonSerializer.Serialize(new
                    {
                        error      = exception.Message,
                        sourceFile = exception.Source,
                        stackTrace = exception.StackTrace,
                    });
                }
            }

            return context.Response.WriteAsync(result);
        }
    }
}
