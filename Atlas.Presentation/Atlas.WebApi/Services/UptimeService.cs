using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Atlas.Application.Services;
using Coravel.Invocable;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace Atlas.WebApi.Services
{
    public class UptimeService : IInvocable
    {
        private readonly IAtlasInfluxDbService _service;

        public UptimeService(IAtlasInfluxDbService service)
        {
            _service = service;
        }

        public Task Invoke()
        {
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;

            _service.Write(write =>
            {
                var point = PointData.Measurement("uptime")
                    .Tag("project", "atlas")
                    .Field("value", uptime.ToString(@"hh\:mm\:ss"))
                    .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                write.WritePoint(point, "atlas_bucket", "atlas");
            });

            return Task.CompletedTask;
        }
    }
}
