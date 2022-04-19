using System;
using Atlas.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Atlas.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider
                        .GetRequiredService<AtlasDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {

                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseWebRoot("wwwroot")
                .UseUrls("http://0.0.0.0:5055")
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .UseKestrel(options =>
                    options.Limits.MaxRequestBodySize = null)
                .ConfigureKestrel((context, options) =>
                    options.AllowSynchronousIO = true);
        }
    }
}
