using System;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using Atlas.Payme.MerchantApi.Settings;
using System.Net;
using Microsoft.Extensions.Options;
using EdjCase.JsonRpc.Router;

namespace Atlas.Payme.MerchantApi.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthSettings    _authSettings;

        public BasicAuthMiddleware(IOptions<AuthSettings> authSettings, RequestDelegate next) =>
            (_authSettings, _next) = (authSettings.Value, next);

        public async Task Invoke(HttpContext httpContext)
        {
            var authHeader = httpContext.Request.Headers["Authorization"].ToString();
            if (authHeader != null)
            {
                var auth     = authHeader.Split(' ')[1];
                var encoding = Encoding.GetEncoding("UTF-8");

                var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                var username = usernameAndPassword.Split(':')[0];
                var password = usernameAndPassword.Split(':')[1];

                if (username == _authSettings.Login && password == _authSettings.Password)
                {
                    await _next(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    var bytes = Encoding.UTF8.GetBytes("{\"id\":0,\"jsonrpc\":\"2.0\",\"error\":{\"code\":-32605,\"message\":\"An Rpc error occurred while trying to invoke request.\",\"data\":\"\"}}");
                    await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    return;
                }
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var bytes = Encoding.UTF8.GetBytes("{\"id\":0,\"jsonrpc\":\"2.0\",\"error\":{\"code\":-32605,\"message\":\"An Rpc error occurred while trying to invoke request.\",\"data\":\"\"}}");
                await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                return;
            }
        }
    }
}

