using System;
using System.Collections.Generic;
using System.Diagnostics;
using Atlas.Application.Services;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Atlas.Identity.Observers
{
    public class DiagnosticObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly IAtlasInfluxDbService _atlasInfluxDbService;

        public DiagnosticObserver(IAtlasInfluxDbService atlasInfluxDbService)
        {
            _atlasInfluxDbService = atlasInfluxDbService;
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (value.Key == "Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop")
            {
                var httpContext = value.Value.GetType().GetProperty("HttpContext")?.GetValue(value.Value) as HttpContext;
                var activity    = Activity.Current;

                var path     = httpContext.Request.Path;
                var duration = activity.Duration.TotalMilliseconds;

                _atlasInfluxDbService.Write(write =>
                {
                    var point = PointData.Measurement("identity_duration")
                        .Tag("path", path)
                        .Field("value", duration)
                        .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                    write.WritePoint(point, "atlas_bucket", "atlas");
                });
            }
        }
    }
}
