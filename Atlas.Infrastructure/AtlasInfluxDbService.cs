using System;
using System.Threading.Tasks;
using Atlas.Application.Services;
using InfluxDB.Client;
using InfluxDB.Client.Core;
using Microsoft.Extensions.Configuration;

namespace Atlas.Persistence
{
    public class AtlasInfluxDbService : IAtlasInfluxDbService
    {
        private readonly string _token;

        public AtlasInfluxDbService(IConfiguration configuration) =>
            _token = configuration["InfluxDb:Token"];

        public void Write(Action<WriteApi> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            client.SetLogLevel(LogLevel.None);
            using var write = client.GetWriteApi();
            action(write);
        }

        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            client.SetLogLevel(LogLevel.None);
            var query = client.GetQueryApi();
            return await action(query);
        }
    }
}
