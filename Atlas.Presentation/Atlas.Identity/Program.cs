using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Atlas.Persistence;

namespace Atlas.Identity
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
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseWebRoot("wwwroot")
                .UseUrls("http://127.0.0.1:5056")
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
